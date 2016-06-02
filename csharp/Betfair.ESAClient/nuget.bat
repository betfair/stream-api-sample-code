

SET CSCPATH=%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319

if not exist ".\nuget.exe" powershell -Command "(new-object System.Net.WebClient).DownloadFile('https://nuget.org/nuget.exe', '.\nuget.exe')"
.\nuget.exe restore

