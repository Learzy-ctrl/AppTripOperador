using AppTripOperador.View;
using AppTripOperador.View.Login;
using Plugin.FirebasePushNotification;
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

            CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
        }

        private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            SecureStorage.SetAsync("DeviceToken", e.Token);
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
