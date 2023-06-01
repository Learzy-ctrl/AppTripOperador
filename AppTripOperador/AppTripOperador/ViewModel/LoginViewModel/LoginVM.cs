﻿using AppTripOperador.View.Login;
using AppTripOperador.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripOperador.ViewModel.LoginViewModel
{
    public class LoginVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public LoginVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes


        public async Task GoToRegister()
        {
            await Navigation.PushAsync(new Register());
        }
        public async Task GoToTabbedpage()
        {
            await Navigation.PushAsync(new TabbPageContainer());
        }
        #endregion

        #region Commands
        public ICommand GoToRegisterCommand => new Command(async () => await GoToRegister());
        public ICommand GoToTabbedpageCommand => new Command(async () => await GoToTabbedpage());
        #endregion
    }
}
