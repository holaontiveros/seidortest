using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using SeidorTestv1.Droid;
using SeidorTestv1;
using System.IO;
using System;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace SeidorTestv1.Droid
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filepath = Path.Combine(documentsPath, customWebView.Uri);
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", filepath));
            }
        }
    }
}
