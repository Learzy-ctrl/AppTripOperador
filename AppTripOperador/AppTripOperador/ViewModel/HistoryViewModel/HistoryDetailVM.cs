using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripOperador.ViewModel.HistoryViewModel
{
    public class HistoryDetailVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public HistoryDetailVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes

        public void GoBack()
        {
            Navigation.PopModalAsync();
        }

        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(GoBack);

        #endregion
    }
}
