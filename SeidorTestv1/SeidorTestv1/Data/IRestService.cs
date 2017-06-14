using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace SeidorTestv1
{
    public interface IRestService
    {
        Task<ObservableCollection<DocumentItem>> RefreshDataAsync();
        Task<string> DownloadFileAsync(string url, IProgress<double> progress, CancellationToken token);
    }
}
