using Acr.UserDialogs;
using AppTripOperador.Data;
using AppTripOperador.Model;
using AppTripOperador.View.Home;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripOperador.ViewModel.HomeViewModel
{
    public class HomeVM : BaseViewModel
    {
        private readonly HomeData data = null;

        #region Variables
        Home page;
        List<TravelModel> _travelList;
        bool _isRefresh = false;
        #endregion

        #region Constructor
        public HomeVM(INavigation navigation, Home page)
        {
            Navigation = navigation;
            this.page = page;
            data = new HomeData();
            RefreshPage();
        }
        #endregion

        #region Objetcs
        public List<TravelModel> TravelList
        {
            get { return _travelList; }
            set { SetValue(ref _travelList, value); }
        }

        public bool isRefresh
        {
            get { return _isRefresh; }
            set { SetValue(ref _isRefresh, value); }
        }
        #endregion

        #region Processes
        public async Task RefreshPage()
        {
            isRefresh = true;
            await Task.Delay(500);
            var list = await data.GetAllAssignmentTravelsAsync();
            if(list != null)
            {
                if(list.Count != 0)
                {
                    page.ChangeView(true, true);
                    TravelList = list;
                }
                else
                {
                    page.ChangeView(false, true);
                }
            }
            else
            {
                page.ChangeView(false, false);
            }
            isRefresh = false;
        }

        public async Task ConfirmTravel(TravelModel model)
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Task.Delay(500);
            model.DateConfirm = DateTime.Now.ToString("dd/MM/yyyy");
            var IsValid = await data.ConfirmTravelAsync(model);
            UserDialogs.Instance.HideLoading();
            if (IsValid)
            {
                await RefreshPage();
                await DisplayAlert("Exito", "Se ha confirmado el viaje asignado", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "Se interrumpio la operacion, intenta de nuevo", "Ok");
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshPageCommand => new Command(async () => await RefreshPage());
        public ICommand ConfirmTravelCommand => new Command<TravelModel>(async (m) => await ConfirmTravel(m));
        #endregion
    }
}
