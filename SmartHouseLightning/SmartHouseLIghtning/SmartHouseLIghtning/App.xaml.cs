using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouseLIghtning.Services;
using SmartHouseLIghtning.Views;

namespace SmartHouseLIghtning
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "https://onlinegradebook.azurewebsites.net";
        public static bool AutoMode = false;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<AzureDataStore>();
            MainPage = new MainPage();
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
