using System;
using System.Collections.Generic;
using System.Text;

namespace RoomiesCalc.Interfaces
{
    public interface IHUDProvider
    {
        void DisplayProgress(string message);

        void DisplaySuccess(string message);

        void DisplayError(string message);

        void Dismiss();
    }
}
