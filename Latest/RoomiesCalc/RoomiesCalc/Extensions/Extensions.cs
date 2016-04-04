using RoomiesCalc.Interfaces;
using RoomiesCalc.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RoomiesCalc
{
    public static class Extensions
    {
        public static void ToToast(this string message, ToastNotificationType type = ToastNotificationType.Info, string title = null)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var toaster = DependencyService.Get<IToastNotifier>();
                toaster.Notify(type, title ?? type.ToString().ToUpper(), message, TimeSpan.FromSeconds(2.5f));
            });
        }

        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static T Get<T>(this ConcurrentDictionary<string, T> dict, string id) where T : BaseModel
        {
            if (id == null)
                return null;

            T v = null;
            dict.TryGetValue(id, out v);
            return v;
        }

        public static void AddOrUpdate<T>(this ConcurrentDictionary<string, T> dict, T model) where T : BaseModel
        {
            if (model == null)
                return;

            //TODO move to an IRefreshable interface
            var athlete = model as Roomie;
            if (athlete != null)
            {
                athlete.LocalRefresh();
            }

            if (dict.ContainsKey(model.Id))
            {
                if (!model.Equals(dict[model.Id]))
                    dict[model.Id] = model;
            }
            else
            {
                dict.TryAdd(model.Id, model);
            }
        }
    }
}
