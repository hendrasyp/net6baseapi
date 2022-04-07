using NET6Base.API.DAO.Implementation;
using NET6Base.API.DAO.Interfaces;

namespace NET6Base.API.Settings
{
    /// <summary>
    /// Application bootstrap installer
    /// </summary>
    public static class AppServiceBootstrap
    {
        /// <summary>
        /// Configured services
        /// </summary>
        /// <param name="services"></param>
        public static void InstallService(this IServiceCollection services)
        {
            services.AddTransient<IFooService, FooRepository>();

            // services.AddScoped<IResponse, ImplResponse>();

        }
    }
}