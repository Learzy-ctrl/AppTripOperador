using Acr.UserDialogs;
using AppTripOperador.View.History;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripOperador.ViewModel.HistoryViewModel
{
    public class HistoryVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public HistoryVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes

        public void GoToHistoryDetail()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            Navigation.PushModalAsync(new HistoryDetail());
            UserDialogs.Instance.HideLoading();
        }

        #endregion

        #region Commands
        public ICommand GoToHistoryDetailCommand => new Command(GoToHistoryDetail);

        #endregion
    }
}
