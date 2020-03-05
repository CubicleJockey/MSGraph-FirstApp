# MSGraph-FirstApp
 Performing a sample of Microsoft Graph

* [Microsoft Graph](https://developer.microsoft.com/en-us/graph)
* [Getting Started Tutorial](https://docs.microsoft.com/en-us/graph/tutorials/dotnet-core)
* [Azure Active Directory](https://aad.portal.azure.com/)

#### dotnet cli commands:
* Tools -> NuGet Package Manager -> Package Manager Console
* Set-Location -Path .\MSGraph-FirstApp
* dotnet user-secrets init
* dotnet user-secrets set appId ".NET Core MSGraph-FirstApp" [Set in Azure Active Directory]
* dotnet user-secrets set scopes "User.Read;Calendars.Read"