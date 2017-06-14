using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)] //enchance speed enable xaml compile

namespace SeidorTestv1
{
    public partial class App : Application
    {
        public static DocumentItemManager DocumentManager { get; private set; }

        public App()
        {
            InitializeComponent();
            DocumentManager = new DocumentItemManager(new RestService());
            MainPage = new NavigationPage(new SeidorTestv1.Views.FileList());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
