namespace NET6Base.API.Settings
{
    /// <summary>
    /// Application Context
    /// </summary>
    public static class AppContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static IHttpContextAccessor _httpContextAccessor;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// ...
        /// </summary>
#pragma warning disable CS8603 // Possible null reference return.
        public static HttpContext Current => _httpContextAccessor.HttpContext;
#pragma warning restore CS8603 // Possible null reference return.
    }
}
