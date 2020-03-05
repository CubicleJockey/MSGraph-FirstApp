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
               
                // Initialize Graph client
                GraphHelper.Initialize(deviceAuthProvider);

                // Get signed in user
                var user = GraphHelper.GetMeAsync().Result;
                WriteLine($"Welcome {user.DisplayName}!{Environment.NewLine}{Environment.NewLine}");

                int choice = -1;
                while (choice != 0)
                {
                    WriteLine("Please choose one of the following options:");
                    WriteLine("\t0. Exit");
                    WriteLine("\t1. Display access token");
                    WriteLine("\t2. List calendar events");


                    var isValidChoice = int.TryParse(ReadLine(), out choice);
                    if (!isValidChoice)
                    {
                        //Invalid options choosing one
                        choice = 1;
                    }

                    //var message = choice switch
                    //{
                    //    0 => "Goodbye...",
                    //    1 => "<Place Holder for Access Token",
                    //    2 => "<Place Holder for Calendar List",
                    //    _ => "Invalid Choice!, Please Try Again"
                    //};
                    switch (choice)
                    {
                        case 0:
                            WriteLine("Goodbye...");
                            break;
                        case 1:
                            WriteLine($"Access token: {accessToken}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}");
                            break;
                        case 2:
                            ListCalendarEvents();
                            break;
                        default:
                            WriteLine("Invalid Choice!, Please Try Again");
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                WriteLine($"Error occured: [{exception.Message}]");
                ReadLine();
                return;
            }
        }

        #region Helper Methods

        private static string FormatDateTimeTimeZone(Microsoft.Graph.DateTimeTimeZone value)
        {
            // Get the timezone specified in the Graph value
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(value.TimeZone);
            // Parse the date/time string from Graph into a DateTime
            var dateTime = DateTime.Parse(value.DateTime);
            // Create a DateTimeOffset in the specific timezone indicated by Graph
            var dateTimeWithTz = new DateTimeOffset(dateTime, timeZone.BaseUtcOffset).ToLocalTime();

            return dateTimeWithTz.ToString("g");
        }

        private static void ListCalendarEvents()
        {
            var events = GraphHelper.GetEventsAsync().Result;

            WriteLine("Events:");

            foreach (var calendarEvent in events)
            {
                WriteLine($"Subject: {calendarEvent.Subject}");
                WriteLine($"\tOrganizer: {calendarEvent.Organizer.EmailAddress.Name}");
                WriteLine($"\tStart: {FormatDateTimeTimeZone(calendarEvent.Start)}");
                WriteLine($"\tEnd: {FormatDateTimeTimeZone(calendarEvent.End)}");
                WriteLine($"{Environment.NewLine}{Environment.NewLine}");
            }
        }

        #endregion Helper Methods
    }
}
