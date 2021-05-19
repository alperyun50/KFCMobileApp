using KFCMobileApp.Pages;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFCMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // one time login function
            var accesstoken = Preferences.Get("AccessToken", string.Empty);
            if(string.IsNullOrEmpty(accesstoken))
            {
                MainPage = new NavigationPage(new SignupPage());
            }
            else
            {
                MainPage = new NavigationPage(new HomePage());
            }

            MainPage = new NavigationPage(new SignupPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
