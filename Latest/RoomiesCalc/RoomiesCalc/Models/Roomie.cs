using RoomiesCalc.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomiesCalc.Models
{
    public class Roomie : BaseModel
    {
        public Roomie(GoogleUserProfile profile)
        {
            Name = profile.Name;
            Email = profile.Email;
            ProfileImageUrl = profile.Picture;
            //Initialize();
        }
        string _userId;
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                SetPropertyChanged(ref _userId, value);
            }
        }

        string _alias;

        public string Alias
        {
            get
            {
                return _alias;
            }
            set
            {
                _alias = value;
            }
        }

        string _name;

        public string Name
        {
            get
            {
                if (RoomieApp.CurrentRoomie != null && RoomieApp.CurrentRoomie.Id != Id && !string.IsNullOrEmpty(_name))
                    return _name.Split(' ')[0];

                return _name;
            }
            set
            {
                SetPropertyChanged(ref _name, value);
            }
        }

        string _email;

        public string Email
        {
            get
            {
                if (RoomieApp.CurrentRoomie != null && RoomieApp.CurrentRoomie.Id != Id)
                    return "user@demo.com";

                return _email;
            }
            set
            {
                SetPropertyChanged(ref _email, value);
            }
        }

        bool _isAdmin;

        public bool IsAdmin
        {
            get
            {
                return _isAdmin;
            }
            set
            {
                SetPropertyChanged(ref _isAdmin, value);
            }
        }

        string _deviceToken;

        public string DeviceToken
        {
            get
            {
                return _deviceToken;
            }
            set
            {
                SetPropertyChanged(ref _deviceToken, value);
            }
        }

        string _devicePlatform;

        public string DevicePlatform
        {
            get
            {
                return _devicePlatform;
            }
            set
            {
                SetPropertyChanged(ref _devicePlatform, value);
            }
        }

        string _notificationRegistrationId;

        public string NotificationRegistrationId
        {
            get
            {
                return _notificationRegistrationId;
            }
            set
            {
                SetPropertyChanged(ref _notificationRegistrationId, value);
            }
        }

        string _profileImageUrl;

        public string ProfileImageUrl
        {
            get
            {
                return _profileImageUrl;
            }
            set
            {
                SetPropertyChanged(ref _profileImageUrl, value);
            }
        }
    }
}
