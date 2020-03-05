using System.Collections.Generic;
using Microsoft.Graph;
using System.Threading.Tasks;
using static System.Console;

namespace MSGraph_FirstApp.GraphHelpers
{
    public static class GraphHelper
    {
        private static GraphServiceClient client;

        public static void Initialize(IAuthenticationProvider authenticationProvider)
        {
            client = new GraphServiceClient(authenticationProvider);
        }
        
        public static async Task<User> GetMeAsync()
        {
            try
            {
                // GET /me
                return await client.Me.Request().GetAsync();
            }
            catch (ServiceException ex)
            {
                WriteLine($"Error getting signed-in user: {ex.Message}");
                return null;
            }
        }

        public static async Task<IEnumerable<Event>> GetEventsAsync()
        {
            try
            {
                // GET /me/events
                var resultPage = await client.Me.Events.Request()
                    // Only return the fields used by the application
                    .Select("subject,organizer,start,end")
                    // Sort results by when they were created, newest first
                    .OrderBy("createdDateTime DESC")
                    .GetAsync();

                return resultPage.CurrentPage;
            }
            catch (ServiceException ex)
            {
                WriteLine($"Error getting events: {ex.Message}");
                return null;
            }
        }
    }
}
