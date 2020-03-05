# MSGraph-FirstApp
 Performing a sample of Microsoft Graph

#### Project Requirements:
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)

#### Documentation Links:
* [Microsoft Graph](https://developer.microsoft.com/en-us/graph)
* [Getting Started Tutorial](https://docs.microsoft.com/en-us/graph/tutorials/dotnet-core)
* [Azure Active Directory](https://aad.portal.azure.com/)
* [dotnet user-secrets Documentation](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows&source=docs)

#### dotnet cli commands:
* Tools -> NuGet Package Manager -> Package Manager Console
* Set-Location -Path .\MSGraph-FirstApp
* dotnet user-secrets init
* dotnet user-secrets set appId ".NET Core MSGraph-FirstApp" [Set in Azure Active Directory]
* dotnet user-secrets set scopes "User.Read;Calendars.Read"