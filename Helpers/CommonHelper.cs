namespace NET6Base.API.Helpers
{
    /// <summary>
    /// Common helper
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// Mendapatkan konfigurasi
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public static string GetConfiguration(string section)
        {
            IConfiguration conf = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());
            return conf.GetSection(section).Value.ToString();
        }
    }
}
