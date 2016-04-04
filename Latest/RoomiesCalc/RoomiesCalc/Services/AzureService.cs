using Microsoft.WindowsAzure.MobileServices;
using ModernHttpClient;
using RoomiesCalc.App;
using RoomiesCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoomiesCalc.Services
{
    public class AzureService
    {
        #region Properties

        static AzureService _instance;

        public static AzureService Instance
        {
            get
            {
                return _instance ?? (_instance = new AzureService());
            }
        }

        MobileServiceClient _client;

        public MobileServiceClient Client
        {
            get
            {
                if (_client == null)
                {
                    var handler = new NativeMessageHandler();

#if __IOS__

					//Use ModernHttpClient for caching and to allow traffic to be routed through Charles/Fiddler/etc
					handler = new ModernHttpClient.NativeMessageHandler() {
						Proxy = CoreFoundation.CFNetwork.GetDefaultProxy(),
						UseProxy = true,
					};

#endif

                    _client = new MobileServiceClient("Keys.AzureDomain", "Keys.AzureApplicationKey", new HttpMessageHandler[] {
                        null,
                        null,
                        handler,
                    });

                    CurrentPlatform.Init();
                }

                return _client;
            }
        }

        #endregion

        #region Push Notifications

        /// <summary>
        /// This app uses Azure as the backend which utilizes Notifications hubs
        /// </summary>
        /// <returns>The athlete notification hub registration.</returns>
        public Task UpdateAthleteNotificationHubRegistration(Roomie roomie, bool forceSave = false, bool sendTestPush = false)
        {
            return new Task(() =>
            {
                if (roomie == null)
                    throw new ArgumentNullException("roomie");

                if (roomie.Id == null || roomie.DeviceToken == null)
                    return;

                var tags = new List<string> {
                    RoomieApp.CurrentRoomie.Id,
                    "All",
                };

                RoomieApp.CurrentRoomie.LocalRefresh();
                //App.CurrentRoomie.Memberships.Select(m => m.LeagueId).ToList().ForEach(tags.Add);
                roomie.DevicePlatform = Xamarin.Forms.Device.OS.ToString();

                var reg = new DeviceRegistration
                {
                    Handle = roomie.DeviceToken,
                    Platform = roomie.DevicePlatform,
                    Tags = tags.ToArray()
                };

                var registrationId = Client.InvokeApiAsync<DeviceRegistration, string>("registerWithHub", reg, HttpMethod.Put, null).Result;
                roomie.NotificationRegistrationId = registrationId;

                //Used to verify the device is successfully registered with the backend 
                if (sendTestPush)
                {
                    var qs = new Dictionary<string, string>();
                    qs.Add("athleteId", roomie.Id);
                    Client.InvokeApiAsync("sendTestPushNotification", null, HttpMethod.Get, qs).Wait();
                }

                if (roomie.IsDirty || forceSave)
                {
                    var task = SaveRoomie(roomie);
                    task.Start();
                    task.Wait();
                }
            });
        }

        public Task UnregisterAthleteForPush(Roomie roomie)
        {
            return new Task(() =>
            {
                if (roomie == null || roomie.NotificationRegistrationId == null)
                    return;

                var values = new Dictionary<string, string> { {
                        "id",
                        roomie.NotificationRegistrationId
                    }
                };
                var registrationId = Client.InvokeApiAsync<string>("unregister", HttpMethod.Delete, values).Result;
            });
        }

        #endregion

        #region Roomies

        public Task<List<Roomie>> GetAllAthletes()
        {
            return new Task<List<Roomie>>(() =>
            {
                DataManager.Instance.Roomies.Clear();
                var list = Client.GetTable<Roomie>().OrderBy(a => a.Name).ToListAsync().Result;
                list.ForEach(a => DataManager.Instance.Roomies.AddOrUpdate(a));
                return list;
            });
        }

        public Task<Roomie> GetAthleteByEmail(string email)
        {
            return new Task<Roomie>(() =>
            {
                var list = Client.GetTable<Roomie>().Where(a => a.Email == email).ToListAsync().Result;
                var roomie = list.FirstOrDefault();

                if (roomie != null)
                    DataManager.Instance.Roomies.AddOrUpdate(roomie);

                return roomie;
            });
        }

        public Task<Roomie> GetAthleteById(string id, bool force = false)
        {
            return new Task<Roomie>(() =>
            {
                Roomie a = null;

                if (!force)
                    DataManager.Instance.Roomies.TryGetValue(id, out a);

                a = a ?? Client.GetTable<Roomie>().LookupAsync(id).Result;

                if (a != null)
                {
                    a.IsDirty = false;
                    DataManager.Instance.Roomies.AddOrUpdate(a);
                }

                return a;
            });
        }

        public Task SaveRoomie(Roomie roomie)
        {
            return new Task(() =>
            {
                roomie.UserId = AzureService.Instance.Client.CurrentUser.UserId;

                if (roomie.Id == null)
                {
                    Client.GetTable<Roomie>().InsertAsync(roomie).Wait();
                }
                else
                {
                    Client.GetTable<Roomie>().UpdateAsync(roomie).Wait();
                }

                DataManager.Instance.Roomies.AddOrUpdate(roomie);
            });
        }

        #endregion
    }
}
