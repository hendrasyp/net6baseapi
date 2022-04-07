namespace NET6Base.API.Settings
{
    /// <summary>
    /// App Setting Getter
    /// </summary>
    public class AppSettings
    {
        public const string SectionName = "ConnectionStrings";
        public string DefaultConnection { get; set; }
        public string ConnectionStrings { get; set; }
        /// <summary>
        /// App Setting Constructor
        /// </summary>
        public AppSettings()
        {
        }
    }
}
