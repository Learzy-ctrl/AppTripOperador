﻿using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTripOperador.Firebase
{
    public class UserRepository
    {
        static string WebAPIkey = "AIzaSyAN9MIGaBRpm7E_8ImP6uEGpkQomJkjfhg";
        FirebaseAuthProvider authProvider;

        public UserRepository()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
        }
        public async Task<string> Register(string email, string password)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                var user = await authProvider.GetUserAsync(token.FirebaseToken);
                return user.LocalId;
            }
            else
            {
                return "";
            }
        }

        public async Task<string> SignIn(string email, string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            var user = await authProvider.GetUserAsync(token.FirebaseToken);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                await SecureStorage.SetAsync("UserID", user.LocalId);
                await SecureStorage.SetAsync("Token", token.FirebaseToken);
                return token.FirebaseToken;
            }
            else
            {
                return "";
            }

        }
    }
}
