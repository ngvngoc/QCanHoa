using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q.Helpers
{
    public class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }
        public static string Jsonuser
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(Jsonuser), null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(Jsonuser), value);
            }
        }
        public static string Jsonshop
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(Jsonshop), null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(Jsonshop), value);
            }
        }
        public static string Jsonluser
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(Jsonluser), null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(Jsonluser), value);
            }
        }
        public static string Jsonlshop
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(Jsonlshop), null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(Jsonlshop), value);
            }
        }
        public static string StringDateSync
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(StringDateSync), null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(StringDateSync), value);
            }
        }

    }
}
