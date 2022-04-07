### CHANGE LOG ###

Referensi:
1. https://jasonwatmore.com/post/2021/04/30/net-5-jwt-authentication-tutorial-with-example-api
2. https://www.c-sharpcorner.com/article/jwt-validation-and-authorization-in-net-5-0/
3. https://stackoverflow.com/questions/182492/how-do-i-configure-the-tortoisesvn-global-ignore-pattern-properly
4. https://referbruv.com/blog/posts/integrating-aspnet-core-api-versions-with-swagger-ui
5. https://forums.asp.net/t/2173093.aspx?+NET+5+using+appsettings+json

```
// OLD APPSETTINGS
{
  "SuperAdminToken": "DIP_HOM_WEBADMIN",
  "FileManagement": {
    "UserUploadDirectory": "Uploads",
    "UseDateYearRoll": 1
  },
  "Jwt": {
    "Key": "SecretKey10125779374235322",
    "Issuer": "https://localhost:44366",
    "Audience": "http://localhost:4200"
  },
  "ApplicationSetting": {
    "Culture": "en-GB",
    "TestMode" : "NO" 
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Error"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ActiveEnvironment": "Default",
    "DefaultConnection": "Server=10.121.35.152\\HOM2014;database=HSN; User ID=sa; Password=123456@rsu1; Connection TimeOut=86400000;"
  },
  "ConnectionStrings_JANGAN_LUPA_INI_KALO_PRODUCTION_MENDING_DIHAPUS_HE": {
    "ActiveEnvironment": "DIPExtron",
    "DefaultConnection": "Server=.;database=HSN; User ID=sa; Password=Serverh5n; Connection TimeOut=86400000;",
    "LocalHost": "Server=.;database=HSN; User ID=sa; Password=Serverh5n; Connection TimeOut=86400000;",
    "DIPExtron": "Server=192.168.10.208;database=HSN; User ID=sa; Password=Serverh5n; Connection TimeOut=86400000;",
    "RSUITest": "Server=10.121.35.152\\HOM2014;database=HSN; User ID=sa; Password=123456@rsu1; Connection TimeOut=86400000;",
    "RSUIProduction": "Server=10.121.35.152\\HOM2014;database=HSN; User ID=sa; Password=123456@rsu1; Connection TimeOut=86400000;",
    "RSHGATest": "Server=10.121.35.152\\HOM2014;database=HSN; User ID=sa; Password=123456@rsu1; Connection TimeOut=86400000;",
    "RSHGAProduction": "Server=10.121.35.152\\HOM2014;database=HSN; User ID=sa; Password=123456@rsu1; Connection TimeOut=86400000;"
  },
  "HangfireCredentials": {
    "UserName": "admin",
    "Password": "Serverh5n"
  }
}

```