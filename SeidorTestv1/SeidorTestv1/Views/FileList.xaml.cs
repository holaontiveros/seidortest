using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeidorTestv1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileList : ContentPage
    {

        public FileList()
        {
            InitializeComponent();

            BindingContext = new FileListViewModel();
        }

        async void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedDoc = ((ListView)sender).SelectedItem as DocumentItem;
            if (selectedDoc != null)
            {
                var page = new DocumentDownloadView();
                page.BindingContext = selectedDoc;
                await Navigation.PushAsync(page);
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _listView.SelectedItem = null;
        }
        
    }
}