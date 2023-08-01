using AppTripOperador.Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTripOperador.Services
{
    public class FirebaseConnection
    {
        public static FirebaseClient firebase = new FirebaseClient("https://mvvmguia-ecc82-default-rtdb.firebaseio.com/",
            new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => RefreshToken()
            });

        public static async Task<string> RefreshToken()
        {
            var token = await SecureStorage.GetAsync("Token");
            if (JwtHelper.DecodeJWT(token))
            {
                return token;
            }
            else
            {
                var auth = new UserRepository();
                var email = SecureStorage.GetAsync("EmailUser").Result;
                var psswrd = SecureStorage.GetAsync("Password").Result;
                var response = await auth.SignIn(email, psswrd);
                return response;
            }
        }
    }

    public class JwtHelper
    {
        public static bool DecodeJWT(string token)
        {
            DateTime date = DateTime.Now;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtDecoded;

            try
            {
                jwtDecoded = handler.ReadJwtToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            foreach (var claim in jwtDecoded.Claims)
            {
                if (claim.Type == "exp")
                {
                    var timestamp = int.Parse(claim.Value);
                    date = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime.ToLocalTime();
                }
            }

            if (date > DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
