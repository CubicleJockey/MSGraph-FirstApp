using System;
using Microsoft.Extensions.Configuration;
using MSGraph_FirstApp.Configuration;
using MSGraph_FirstApp.GraphHelpers;
using static System.Console;

namespace MSGraph_FirstApp
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; }

        static Program()
        {
            Configuration = HostConfiguration.LoadApplicationSettings();
        }

        public static void Main()
        {
            WriteLine($".NET Core Graph First App{Environment.NewLine}");

            if (Configuration == null)
            {
                WriteLine("Invalid or missing user-secrets store. Press any key to exit...");
                ReadLine();
                return;
            }

            var isValidApplicationId = Guid.TryParse(Configuration[ConfigurationSettings.ApplicationClientId], out var applicationClientId);
            var scopeSettings = Configuration[ConfigurationSettings.Scopes];
            var scopes = scopeSettings.Split(';');

            if (!isValidApplicationId
                || string.IsNullOrWhiteSpace(scopeSettings))
            {
                WriteLine($"{ConfigurationSettings.ApplicationClientId} and {ConfigurationSettings.Scopes} are required.");
                WriteLine("Press any key to exit...");
                ReadLine();
                return;
            }

            try
            {
                var deviceAuthProvider = new DeviceCodeAuthProvider(applicationClientId, scopes);
                var accessToken = deviceAuthProvider.GetAccessTokens().Result;
                WriteLine($"Access token: {accessToken}{Environment.NewLine}");

                var isValidChoice = int.TryParse(ReadLine(), out var choice);
                if (!isValidChoice)
                {
                    //Invalid options choosing one
                    choice = 1;
                }

                var message = choice switch
                {
                    0 => "Goodbye...",
                    1 => "<Place Holder for Access Token",
                    2 => "<Place Holder for Calendar List",
                    _ => "Invalid Choice!, Please Try Again"
                };

                WriteLine(message);
            }
            catch (Exception exception)
            {
                WriteLine($"Error occured: [{exception.Message}]");
                ReadLine();
                return;
            }
        }

        //TODO: https://docs.microsoft.com/en-us/graph/tutorials/dotnet-core?tutorial-step=3 (Sign in and display the access token section)
    }
}
