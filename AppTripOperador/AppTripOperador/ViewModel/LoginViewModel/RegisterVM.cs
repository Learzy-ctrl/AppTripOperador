using Acr.UserDialogs;
using AppTripOperador.Data.Account;
using AppTripOperador.Firebase;
using AppTripOperador.Model;
using AppTripOperador.View;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTripOperador.ViewModel.LoginViewModel
{
    public class RegisterVM : BaseViewModel
    {
        private readonly UserRepository repository = null;
        private readonly UserData data = null;
        #region Constructor
        public RegisterVM(INavigation navigation)
        {
            Navigation = navigation;
            repository = new UserRepository();
            data = new UserData();
        }
        #endregion

        #region Variables
        string _name;
        string _lastname;
        string _email;
        string _phonenumber;
        string _password;
        string _confirmpassword;
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
        public string Password
        {
            get { return _password; }
            set { SetValue(ref _password, value); }
        }
        public string ConfirmPassword
        {
            get { return _confirmpassword; }
            set { SetValue(ref _confirmpassword, value); }
        }
        #endregion

        #region Processes


        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }
        public async Task RegisterMethod()
        {
            if (String.IsNullOrEmpty(Name))
            {
                await DisplayAlert("Alerta", "Escribe un Nombre", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(LastName))
            {
                await DisplayAlert("Alerta", "Escribe tus apellidos", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(Email))
            {
                await DisplayAlert("Alerta", "Escribe un email", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(PhoneNumber))
            {
                await DisplayAlert("Alerta", "Escribe un numero de telefono", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(Password))
            {
                await DisplayAlert("Alerta", "Escribe una Contraseña", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(ConfirmPassword))
            {
                await DisplayAlert("Alerta", "Confirma tu contraseña", "Ok");
                return;
            }
            if (Password.Count() < 6)
            {
                await DisplayAlert("Alerta", "La contraseña debe ser mayor a 6 caracteres", "Ok");
                return;
            }
            if (Password == ConfirmPassword)
            {
                var newUser = new UserModel();
                newUser.Name = Name;
                newUser.LastName = LastName;
                newUser.PhoneNumber = PhoneNumber;
                newUser.Email = Email;
                newUser.Password = Password;
                newUser.Rol = "3";

                UserDialogs.Instance.ShowLoading("Cargando");
                try
                {
                    string id = await repository.Register(Email, Password);
                    if (id != "")
                    {
                        await SecureStorage.SetAsync("EmailUser", Email);
                        await SecureStorage.SetAsync("Password", Password);
                        newUser.IdUser = id;
                        await data.InsertUser(newUser);

                        await repository.SignIn(Email, Password);
                        await Navigation.PushAsync(new PageEmpty());
                        await Task.Delay(2000);
                        Application.Current.MainPage = new TabbPageContainer();
                        await Task.Delay(1400);
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Falla", "Error al Registrar Usuario", "Ok");
                    }
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("INVALID_EMAIL"))
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Error", "Correo electronico invalido", "ok");
                    }
                    if (e.Message.Contains("EMAIL_EXISTS"))
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Error", "El correo ya esta registrado", "ok");
                    }
                }

            }
            else
            {
                await DisplayAlert("Alerta", "Las contraseñas no coinciden", "Ok");
            }

        }
        #endregion

        #region Commands
        public ICommand RegisterMethodCommand => new Command(async () => await RegisterMethod());
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        #endregion
    }
}
