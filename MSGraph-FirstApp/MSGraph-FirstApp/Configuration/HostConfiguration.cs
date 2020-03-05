using Microsoft.Extensions.Configuration;

namespace MSGraph_FirstApp.Configuration
{
    /// <summary>
    /// Host Configuration
    /// </summary>
    public static class HostConfiguration
    {
        /// <summary>
        /// Load application settings
        ///
        /// I.E -> Use dotnet user-secrets storage
        /// </summary>
        /// <returns>Application Settings as IConfiguration</returns>
        public static IConfigurationRoot LoadApplicationSettings()
        {
            var settings = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            // Check for required settings
            if (string.IsNullOrWhiteSpace(settings[ConfigurationSettings.ApplicationClientId]) 
                || string.IsNullOrWhiteSpace(settings[ConfigurationSettings.Scopes]))
            {
                return null;
            }

            return settings;
        }
    }
}
