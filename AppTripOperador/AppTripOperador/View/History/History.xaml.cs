using AppTripOperador.ViewModel.HistoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripOperador.View.History
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class History : ContentPage
    {
        public History()
        {
            InitializeComponent();
            BindingContext = new HistoryVM(Navigation, this);
        }

        public void RefreshView(bool flag, bool InternetCheck)
        {
            if (InternetCheck)
            {
                if (flag)
                {
                    HistoryView.Content = new WithHistory();
                }
                else
                {
                    HistoryView.Content = new WithoutHistory();
                }
            }
            else
            {
                HistoryView.Content = new WithoutInternet();
            }
        }
    }
}