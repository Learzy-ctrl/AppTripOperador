using AppTripOperador.Model;
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
        TravelModel _historyModel;
        #endregion

        #region Constructor
        public HistoryDetailVM(INavigation navigation, TravelModel model)
        {
            Navigation = navigation;
            HistoryModel = model;
        }
        #endregion

        #region Objetcs
        public TravelModel HistoryModel
        {
            get { return _historyModel; }
            set { SetValue(ref _historyModel, value); }
        }
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
