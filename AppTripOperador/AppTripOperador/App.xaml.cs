using AppTripOperador.View;
using AppTripOperador.View.Login;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripOperador
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var userID = SecureStorage.GetAsync("UserID").Result;
            if (!string.IsNullOrEmpty(userID))
            {
                MainPage = new TabbPageContainer();
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
            //MainPage = new TabbPageContainer();
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
