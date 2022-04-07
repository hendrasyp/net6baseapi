namespace NET6Base.API.Settings.DBDrivers
{
    public class DbCredentialInfo
    {
        /// <summary>
        ///
        /// </summary>
        public string DbHost { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string DbName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string UserId { get; set; } = "";
        /// <summary>
        ///
        /// </summary>
        public string UserPass { get; set; } = "";
        /// <summary>
        ///
        /// </summary>
        public bool IntegratedSecurity { get; set; } = false;
    }
}
