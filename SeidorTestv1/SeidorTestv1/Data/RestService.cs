using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace SeidorTestv1
{
    public class RestService : IRestService
    {
        HttpClient client;

        public ObservableCollection<DocumentItem> Items { get; private set; }

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<ObservableCollection<DocumentItem>> RefreshDataAsync()
        {
            Items = new ObservableCollection<DocumentItem>();

            var uri = new Uri(Constants.RestUrl);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<ObservableCollection<DocumentItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                Debug.WriteLine("--------------------------------------");
            }

            return Items;
        }

        public async Task<string> DownloadFileAsync(string url, IProgress<double> progress, CancellationToken token)
        {
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("The request returned with HTTP status code {0}", response.StatusCode));
            }

            var total = response.Content.Headers.ContentLength.HasValue ? response.Content.Headers.ContentLength.Value : -1L;
            var canReportProgress = total != -1 && progress != null;

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var totalRead = 0L;
                var buffer = new byte[4096];
                var isMoreToRead = true;
                var fileContent = new byte[total];

                do
                {
                    token.ThrowIfCancellationRequested();

                    var read = await stream.ReadAsync(buffer, 0, buffer.Length, token);

                    if (read == 0)
                    {
                        isMoreToRead = false;
                    }
                    else
                    {
                        Buffer.BlockCopy(buffer, 0, fileContent, (int)totalRead, read);

                        totalRead += read;

                        if (canReportProgress)
                        {
                            progress.Report((totalRead * 1d) / (total * 1d));
                        }
                    }
                } while (isMoreToRead);

                var fileURI = new Uri(url);
                var filename = System.IO.Path.GetFileName(fileURI.AbsolutePath);

                await DependencyService.Get<ISavingFile>().Save(filename, fileContent, token);

                return filename;
            }
        }

    }
}
