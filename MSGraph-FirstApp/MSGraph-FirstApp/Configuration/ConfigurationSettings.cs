namespace MSGraph_FirstApp.Configuration
{
    /// <summary>
    /// Configuration Constants
    ///
    /// Values must be set-up in the User-Secrets dotnet command.
    /// </summary>
    public static class ConfigurationSettings
    {
        public static string ApplicationClientId = nameof(ApplicationClientId);
        public static string Scopes = nameof(Scopes);
    }
}
