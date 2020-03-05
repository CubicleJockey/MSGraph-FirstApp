using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using static System.Console;

namespace MSGraph_FirstApp.GraphHelpers
{
    public class DeviceCodeAuthProvider : IAuthenticationProvider
    {
        private readonly IPublicClientApplication msaClientApplication;
        private IAccount userAccount;
        private readonly IEnumerable<string> scopes;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="applicationClientId">Application Client Id (GUID)</param>
        /// <param name="scopes"></param>
        public DeviceCodeAuthProvider(Guid applicationClientId, IEnumerable<string> scopes)
        {
            this.scopes = scopes;

            msaClientApplication = 
                PublicClientApplicationBuilder.Create(applicationClientId.ToString())
                                              .WithAuthority(AadAuthorityAudience.AzureAdAndPersonalMicrosoftAccount)
                                              .Build();
        }

        public async Task<string> GetAccessTokens()
        {
            //No account must log-in
            if (userAccount == null)
            {
                try
                {
                    var tokenFromDevice = await msaClientApplication.AcquireTokenWithDeviceCode(scopes, callback =>
                    {
                        WriteLine(callback.Message);
                        return Task.FromResult(0);
                    }).ExecuteAsync();

                    userAccount = tokenFromDevice.Account;
                    return tokenFromDevice.AccessToken;
                }
                catch (Exception exception)
                {
                    WriteLine($"Error getting access token: {exception.Message}");
                    return null;
                }
            }

            var silentToken = await msaClientApplication.AcquireTokenSilent(scopes, userAccount).ExecuteAsync();
            return silentToken.AccessToken;
        }


        #region Implementation of IAuthenticationProvider

        /// <inheritdoc />
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", await GetAccessTokens());
        }

        #endregion
    }
}
