using System;
using Xamarin.Forms;

namespace SeidorTestv1
{
    public class WebViewPageCS : ContentPage
    {
        public WebViewPageCS(string filename)
        {
            Padding = new Thickness(0, 0, 0, 0);
            Content = new StackLayout
            {
                Children = {
                new CustomWebView {
                    Uri = filename,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                }
            }
            };
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }

}