using RoomiesCalc.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace RoomiesCalc.Services
{
    public class DataManager
    {
        /// <summary>
        /// Data is cached locally in memory due the nature of ever-evolving leaderboards and athlete details
        /// Models are stored in the DataManager's hashtables and typically referenced using the Model.ID (key)
        /// This allows any ViewModel to update a model and all other pages will reflect the updated properties
        /// This could easily be converted to SQLite or Akavache
        /// </summary>
        public DataManager()
        {
            Roomies = new ConcurrentDictionary<string, Roomie>();
        }

        static DataManager _instance;

        public static DataManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataManager();

                return _instance;
            }
        }

        #region Properties

        public ConcurrentDictionary<string, Roomie> Roomies
        {
            get;
            set;
        }

        #endregion
    }
}
