using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using SeidorTestv1.Models;

namespace SeidorTestv1
{
    public class FileListViewModel : NotifyPropertyChangedBase
    {       
       public ObservableCollection<DocumentItem> Documents { get; private set; }
       public ICommand RefreshCommand { get; private set; }
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }

            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isThereItems;
        public bool IsThereItems
        {
            get { return _isThereItems; }

            private set
            {
                if (_isThereItems != value)
                {
                    _isThereItems = value;
                    OnPropertyChanged();
                }
            }
        }

        public FileListViewModel()
       {
            Documents = new ObservableCollection<DocumentItem>();
            RefreshCommand = new Command(async() => getDocuments());
            IsLoading = false;
            IsThereItems = false;
       }

        private async Task getDocuments()
        {
            Documents.Clear();
            IsLoading = true;
            IsThereItems = true;
            var documents = await App.DocumentManager.GetDocumentsAsync();
            var count = 0;
            foreach(var item in documents)
            {
                Documents.Add(item);
                count++;
            }
            IsThereItems = (count > 0) ? true : false;
            IsLoading = false;
        }


    }
}
