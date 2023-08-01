using AppTripOperador.Model;
using AppTripOperador.Services;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTripOperador.Data
{
    public class HomeData
    {
        public async Task<List<TravelModel>> GetAllAssignmentTravelsAsync()
        {
            var userID = SecureStorage.GetAsync("UserID").Result;
            try
            {
                var travels = await FirebaseConnection.firebase.Child("AssignedTrips").Child(userID).OnceAsync<TravelModel>();
                var list = new List<TravelModel>();
                foreach (var t in travels)
                {
                    list.Add(t.Object);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ConfirmTravelAsync(TravelModel model)
        {
            var userID = SecureStorage.GetAsync("UserID").Result;
            try
            {
                var response = await FirebaseConnection.firebase.Child("ConfirmedAssignments").Child(userID).PostAsync(model);
                await FirebaseConnection.firebase.Child("AssignedTrips").Child(userID).Child(model.Key).DeleteAsync();
                model.Key = response.Key;
                await FirebaseConnection.firebase.Child("ConfirmedAssignments").Child(userID).Child(response.Key).PutAsync(model);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
