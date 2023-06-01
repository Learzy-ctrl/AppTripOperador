using AppTripOperador.View;
using AppTripOperador.View.Login;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripOperador
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());
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
