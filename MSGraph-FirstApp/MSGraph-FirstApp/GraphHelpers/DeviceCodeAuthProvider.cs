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
        private const string EMPTYERROR = "Cannot be empty.";

        private readonly IPublicClientApplication msaClientApplication;
        private IAccount userAccount;
        private readonly IEnumerable<string> scopes;
        private readonly Guid applicationClientId;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="applicationClientId">Application Client Id (GUID)</param>
        /// <param name="scopes"></param>
        public DeviceCodeAuthProvider(Guid applicationClientId, IEnumerable<string> scopes)
        {
            this.scopes = scopes ?? throw new ArgumentNullException(nameof(scopes));
            if(applicationClientId == Guid.Empty) { throw new ArgumentException(EMPTYERROR, nameof(applicationClientId)); }

            this.applicationClientId = applicationClientId;

            msaClientApplication = 
                PublicClientApplicationBuilder.Create(this.applicationClientId.ToString())
                                              .WithAuthority(AadAuthorityAudience.AzureAdAndPersonalMicrosoftAccount)
                                              .Build();
        }

        /// <summary>
        /// Ctor Overload
        /// </summary>
        /// <param name="userAccount">User Account</param>
        /// <param name="msaClientApplication">Public Client Application</param>
        /// <param name="applicationClientId">Application Client Id</param>
        /// <param name="scopes">Scopes</param>
        public DeviceCodeAuthProvider(
            IAccount userAccount,
            IPublicClientApplication msaClientApplication,
            Guid applicationClientId,
            IEnumerable<string> scopes)
        {
            this.userAccount = userAccount ?? throw new ArgumentNullException(nameof(userAccount));
            this.msaClientApplication = msaClientApplication ?? throw new ArgumentNullException(nameof(msaClientApplication));

            if(applicationClientId == Guid.Empty) { throw new ArgumentException(EMPTYERROR, nameof(applicationClientId));}
            this.applicationClientId = applicationClientId;
            
            this.scopes = scopes ?? throw new ArgumentNullException(nameof(scopes));

        }

        public async Task<string> GetAccessTokens()
        {
            //No userAccount must log-in
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
