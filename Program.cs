using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NET6Base.API.Helpers;
using NET6Base.API.Settings;
using NET6Base.API.Settings.Swagger;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(ConfigurationHelper.AppCulture);
    options.SupportedCultures = new List<CultureInfo> { new CultureInfo(ConfigurationHelper.AppCulture) };
    options.RequestCultureProviders.Clear();
});

services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//AppSettings settings = new AppSettings();
//services.Configure<AppSettings>(Configuration.GetSection(AppSettings.SectionName));

services.AddHttpContextAccessor();

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

services.InstallService();

services.SwaggerInstall();

services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
    options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
});

services.AddControllers();
services.AddControllers().AddNewtonsoftJson();
services.AddControllers(o =>
{
    o.Conventions.Add(new ControllerDocumentationConvention());
});

#region CORS
services.AddCors();
services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
            builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithMethods("GET", "PUT", "DELETE", "POST", "PATCH") //not really necessary when AllowAnyMethods is used.
            );
});
#endregion

#region COMPRESSION
services.GZIPResponseCompression_Run();
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

#region SERILOG
// https://www.claudiobernasconi.ch/2022/01/28/how-to-use-serilog-in-asp-net-core-web-api/
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#endregion

#region APP BUILDER
var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
var accessor = app.Services.GetRequiredService<IHttpContextAccessor>();
var appBuilder = app.Services.GetRequiredService<IApplicationBuilder>();

app.UseSwagger(c =>
{
    c.RouteTemplate = "docs/api/{documentname}/swagger.json";
});

app.UseSwaggerUI(options =>
    {
        options.InjectStylesheet(Path.Combine(System.AppContext.BaseDirectory, "/swagger-ui/theme-flattop.css"));
        options.InjectStylesheet(Path.Combine(System.AppContext.BaseDirectory, "/swagger-ui/dip5customs.css"));
        options.InjectJavascript(Path.Combine(System.AppContext.BaseDirectory, "/dip5customs.js"));


        for (int idx = 0; idx < provider.ApiVersionDescriptions.Count; idx++)
        {
            options.SwaggerEndpoint(
                $"/docs/api/{provider.ApiVersionDescriptions[idx].GroupName}/swagger.json",
                provider.ApiVersionDescriptions[idx].GroupName.ToUpperInvariant());

            options.RoutePrefix = "docs/api";
        }
    });

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

NET6Base.API.Settings.AppContext.Configure(appBuilder.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

app.UseRequestLocalization();

app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion