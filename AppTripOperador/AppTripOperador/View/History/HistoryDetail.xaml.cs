using AppTripOperador.Model;
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
    public partial class HistoryDetail : ContentPage
    {
        public HistoryDetail(TravelModel model)
        {
            InitializeComponent();
            BindingContext = new HistoryDetailVM(Navigation, model);
        }
    }
}