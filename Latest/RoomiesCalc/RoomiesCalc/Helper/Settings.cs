using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using RoomiesCalc.App;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RoomiesCalc.Helper
{
    public class Settings
    {
        static Settings _instance;
        static readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "settings.json");

        public static Settings Instance
        {
            get
            {
                return _instance ?? (_instance = Settings.Load());
            }
        }

        public string AuthUserID
        {
            get;
            set;
        }

        public string RoomieID
        {
            get;
            set;
        }

        public bool RegistrationComplete
        {
            get;
            set;
        }

        public string DeviceToken
        {
            get;
            set;
        }

        public MobileServiceUser User
        {
            get;
            set;
        }

        public Task Save()
        {
            return Task.Factory.StartNew(() =>
            {
                if (RoomieApp.CurrentRoomie != null)
                    DeviceToken = RoomieApp.CurrentRoomie.DeviceToken;

                Debug.WriteLine(string.Format("Saving settings: {0}", _filePath));
                var json = JsonConvert.SerializeObject(this);
                using (var sw = new StreamWriter(_filePath, false))
                {
                    sw.Write(json);
                }
            });
        }

        public static Settings Load()
        {
            Debug.WriteLine(string.Format("Loading settings: {0}", _filePath));
            var settings = Helpers.LoadFromFile<Settings>(_filePath) ?? new Settings();
            return settings;
        }
    }
}
