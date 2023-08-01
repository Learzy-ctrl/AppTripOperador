using AppTripOperador.ViewModel.HomeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripOperador.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            BindingContext = new HomeVM(Navigation, this);
        }

        public void ChangeView(bool flag, bool IsConected)
        {
            if (IsConected)
            {
                if (flag)
                {
                    TravelView.Content = new WithTravel();
                }
                else
                {
                    TravelView.Content = new WithoutTravel();
                }
            }
            else
            {
                TravelView.Content = new WithoutInternet();
            }
        }
    }
}