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
    public class HistoryData
    {
        public async Task<List<TravelModel>> GetAllTravelHistory()
        {
            var userID = SecureStorage.GetAsync("UserID").Result;
            try
            {
                var travelCollection = await FirebaseConnection.firebase.Child("ConfirmedAssignments").Child(userID).OnceAsync<TravelModel>();
                var list = new List<TravelModel>();
                foreach(var t in travelCollection)
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
    }
}
