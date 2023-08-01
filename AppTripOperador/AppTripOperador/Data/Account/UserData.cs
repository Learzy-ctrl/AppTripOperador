using AppTripOperador.Model;
using AppTripOperador.Services;
using Firebase.Auth;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTripOperador.Data.Account
{
    public class UserData
    {
        public async Task InsertUser(UserModel user)
        {
            await FirebaseConnection.firebase.Child("LocalUsers").Child(user.IdUser).PutAsync(user);
        }

        public async Task<UserModel> GetUser()
        {
            try
            {
                var userID = SecureStorage.GetAsync("UserID").Result;
                var User = await FirebaseConnection.firebase.Child("LocalUsers").Child(userID).OnceSingleAsync<UserModel>();
                return User;
            }
            catch
            {
                return null;
            }

        }
    }
}
