using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeidorTestv1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentDownloadView : ContentPage
    {
        public Microsoft.Progress<double> progress;

        public DocumentDownloadView()
        {
            InitializeComponent();
            BindingContext = this;
            progress = new Microsoft.Progress<double>();
            progress.ProgressChanged += (sender, value) => progress1.Progress = value;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var cancellationToken = new CancellationToken();

            var file = this.BindingContext as DocumentItem;
            System.Diagnostics.Debug.WriteLine("Url: {0}", file.Url);

            var filename = await App.DocumentManager.DownloadFileAsync(file.Url, progress, cancellationToken);

            var answer = await DisplayAlert("Descarga", "El archivo ha sido descargado", "OK", "ABRIR");
            if (!answer)
            {
                await Navigation.PushAsync(new WebViewPageCS(filename));
            }

        }
    }
}