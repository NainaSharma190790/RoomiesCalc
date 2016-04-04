using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomiesCalc.Interfaces
{
    public interface IAuthentication
    {
        Task<MobileServiceUser> DisplayWebView();

        void ClearCookies();
    }
}
