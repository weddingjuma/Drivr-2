using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Drivr.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants

        private const string SettingsKey = "drivr_settings";
        private static readonly string SettingsDefault = string.Empty;

        #endregion

        public static string GeneralSettings
        {
            get { return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault); }
            set { AppSettings.AddOrUpdateValue(SettingsKey, value); }
        }
    }
}