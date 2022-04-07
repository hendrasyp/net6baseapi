namespace NET6Base.API.Helpers
{
    /// <summary>
    /// Helper untuk memanggil parameter key/value dari app.settings
    /// </summary>
    public class ConfigurationHelper
    {
        private static readonly string appSettingKey = "ApplicationSetting";
        public static string BPJSResponseType
        {
            get { return CommonHelper.GetConfiguration("ApplicationSetting:BPJSResponseStatus"); }
        }

        /// <summary>
        /// Default Culture
        /// </summary>
        public static string AppCulture
        {
            get { return CommonHelper.GetConfiguration($"{appSettingKey}:CULTURE"); }
        }
        /// <summary>
        /// Mengetahui apakah sedang di debug atau tidak
        /// </summary>
        public static string IsTestMode
        {
            get { return CommonHelper.GetConfiguration($"{appSettingKey}:TESTMODE"); }
        }
    }
}
