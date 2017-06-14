using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace SeidorTestv1
{
    public class DocumentItemManager
    {
        IRestService restService;

        public DocumentItemManager(IRestService service)
        {
            restService = service;
        }

        public Task<ObservableCollection<DocumentItem>> GetDocumentsAsync()
        {
            return restService.RefreshDataAsync();
        }

        public Task<string> DownloadFileAsync(string url, IProgress<double> progress, CancellationToken token)
        {
            return restService.DownloadFileAsync(url, progress, token);
        }

    }
}
