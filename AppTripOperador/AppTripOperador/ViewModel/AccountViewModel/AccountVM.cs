using Acr.UserDialogs;
using AppTripOperador.Data.Account;
using AppTripOperador.Firebase;
using AppTripOperador.View.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTripOperador.ViewModel.AccountViewModel
{
    public class AccountVM : BaseViewModel
    {
        private UserData Data = null;
        private UserRepository user = null;

        #region Constructor
        public AccountVM(INavigation navigation)
        {
            Data = new UserData();
            user = new UserRepository();
            Navigation = navigation;
            PrintUserData();
        }
        #endregion

        #region Variables
        string _name;
        string _lastname;
        string _email;
        string _phonenumber;
        string _password;
        string _nameandlastname;
        string _userid;
        #endregion

        #region Objetcs
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }
        public string LastName
        {
            get { return _lastname; }
            set { SetValue(ref _lastname, value); }
        }
        public string Email
        {
            get { return _email; }
            set { SetValue(ref _email, value); }
        }
        public string PhoneNumber
        {
            get { return _phonenumber; }
            set { SetValue(ref _phonenumber, value); }
        }
        public string NameAndLastName
        {
            get { return _nameandlastname; }
            set { SetValue(ref _nameandlastname, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetValue(ref _password, value); }
        }
        public string UserID
        {
            get { return _userid; }
            set { SetValue(ref _userid, value); }
        }
        #endregion

        #region Processes


        public async Task CloseSession()
        {

            var IsValid = await DisplayAlert("Cerrar Sesion", "¿Estas seguro de cerrar sesion?", "Si", "No");

            if (IsValid)
            {
                UserDialogs.Instance.ShowLoading("Cerrando Sesion");
                await SecureStorage.SetAsync("UserID", "");
                await SecureStorage.SetAsync("NameUser", "null");
                await SecureStorage.SetAsync("EmailUser", "");
                await SecureStorage.SetAsync("PhoneUser", "");
                await SecureStorage.SetAsync("Password", "");
                await SecureStorage.SetAsync("Token", "");
                Application.Current.MainPage = new NavigationPage(new Login());
                UserDialogs.Instance.HideLoading();
            }
        }

        public async void PrintUserData()
        {
            var User = await Data.GetUser();
            var name = SecureStorage.GetAsync("NameUser").Result;
            if (string.IsNullOrEmpty(name) || name == "null")
            {
                var username = User.Name + " " + User.LastName;
                await SecureStorage.SetAsync("NameUser", username);
                await SecureStorage.SetAsync("EmailUser", User.Email);
                await SecureStorage.SetAsync("PhoneUser", User.PhoneNumber);
                await SecureStorage.SetAsync("Password", User.Password);
                NameAndLastName = User.Name + " " + User.LastName; ;
                Email = User.Email;
                PhoneNumber = User.PhoneNumber;
            }
            else
            {
                var email = SecureStorage.GetAsync("EmailUser").Result;
                var phone = SecureStorage.GetAsync("PhoneUser").Result;
                NameAndLastName = name;
                Email = email;
                PhoneNumber = phone;
            }
        }
        #endregion

        #region Commands
        public ICommand CloseSessionCommand => new Command(async () => await CloseSession());

        #endregion
    }
}
