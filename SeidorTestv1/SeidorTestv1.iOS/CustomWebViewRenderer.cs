/*
* This Class is used to implement the final web viewer for the pdf. This approach was taken from
* Xamarin examples, but it was modified to retrieve files from the filesystem instead of the assets folder.
*/

using System.IO;
using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SeidorTestv1.iOS;
using SeidorTestv1;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace SeidorTestv1.iOS
{
    public class CustomWebViewRenderer : ViewRenderer<CustomWebView, UIWebView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new UIWebView());
            }
            if (e.OldElement != null)
            {
                // Cleanup
            }
            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fileName = Path.Combine(documentsPath, customWebView.Uri);
                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
                Control.ScalesPageToFit = true;
            }
        }
    }
}