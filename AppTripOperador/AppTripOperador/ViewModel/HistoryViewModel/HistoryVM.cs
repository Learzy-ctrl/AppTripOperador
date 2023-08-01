using Acr.UserDialogs;
using AppTripOperador.Data;
using AppTripOperador.Model;
using AppTripOperador.View.History;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripOperador.ViewModel.HistoryViewModel
{
    public class HistoryVM : BaseViewModel
    {
        private readonly HistoryData data = null;

        #region Variables
        List<TravelModel> _listHistory;
        bool _isRefresh = false;
        History page;
        #endregion

        #region Constructor
        public HistoryVM(INavigation navigation, History page)
        {
            Navigation = navigation;
            this.page = page;
            data = new HistoryData();
            RefreshPage();
        }
        #endregion

        #region Objetcs
        public List<TravelModel> ListHistory
        {
            get { return _listHistory; }
            set { SetValue(ref _listHistory, value); }
        }

        public bool IsRefresh
        {
            get { return _isRefresh; }
            set { SetValue(ref _isRefresh, value); }
        }
        #endregion

        #region Processes

        public async Task RefreshPage()
        {
            IsRefresh = true;
            var list = await data.GetAllTravelHistory();
            if (list != null)
            {
                if (list.Count != 0)
                {
                    ListHistory = list;
                    page.RefreshView(true, true);
                }
                else
                {
                    page.RefreshView(false, true);
                }
            }
            else
            {
                page.RefreshView(false, false);
            }
            IsRefresh = false;
        }

        public async Task GoToHistoryDetail(TravelModel model)
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Navigation.PushModalAsync(new HistoryDetail(model));
            UserDialogs.Instance.HideLoading();
        }

        #endregion

        #region Commands
        public ICommand GoToHistoryDetailCommand => new Command<TravelModel>(async (m) => await GoToHistoryDetail(m));
        public ICommand RefreshPageCommand => new Command(async () => await RefreshPage());
        #endregion
    }
}
