using System;
using System.Collections.Generic;
using System.Text;

namespace RoomiesCalc.Models
{
    public class DeviceRegistration
    {
        public string Platform
        {
            get;
            set;
        }

        public string Handle
        {
            get;
            set;
        }

        public string[] Tags
        {
            get;
            set;
        }
    }

    public class NotificationPayload
    {
        public NotificationPayload()
        {
            Payload = new Dictionary<string, string>();
        }

        public string Action
        {
            get;
            set;
        }

        public Dictionary<string, string> Payload
        {
            get;
            set;
        }
    }

    public struct PushActions
    {
        public static string ChallengePosted = "ChallengePosted";
        public static string ChallengeRevoked = "ChallengeRevoked";
        public static string ChallengeAccepted = "ChallengeAccepted";
        public static string ChallengeDeclined = "ChallengeDeclined";
        public static string ChallengeCompleted = "ChallengeCompleted";
        public static string LeagueStarted = "LeagueStarted";
        public static string LeagueEnded = "LeagueEnded";
        public static string LeagueEnrollmentStarted = "LeagueEnrollmentStarted";
    }
}
