using AppOwnsData.Models;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace AppOwnsData.Services
{
    public class AadService(IOptions<AzureAd> azureAd)
    {
        private readonly IOptions<AzureAd> azureAd = azureAd;

        /// <summary>
        /// Generates and returns Access token
        /// </summary>
        /// <returns>AAD token</returns>
        public async Task<string> GetAccessToken()
        {
            AuthenticationResult authenticationResult;
            if (azureAd.Value.AuthenticationMode.Equals("masteruser", StringComparison.InvariantCultureIgnoreCase))
            {
                // Create a public client to authorize the app with the AAD app
                IPublicClientApplication clientApp = PublicClientApplicationBuilder.Create(azureAd.Value.ClientId).WithAuthority(azureAd.Value.AuthorityUrl).Build();
                var userAccounts = await clientApp.GetAccountsAsync();

                try
                {
                    // Retrieve Access token from cache if available
                    authenticationResult = await clientApp.AcquireTokenSilent(azureAd.Value.ScopeBase, userAccounts.FirstOrDefault()).ExecuteAsync();
                }
                catch (MsalUiRequiredException)
                {
                    authenticationResult = await clientApp.AcquireTokenByUsernamePassword(azureAd.Value.ScopeBase, azureAd.Value.PbiUsername, azureAd.Value.PbiPassword).ExecuteAsync();
                }
            }

            // Service Principal auth is the recommended by Microsoft to achieve App Owns Data Power BI embedding
            else if (azureAd.Value.AuthenticationMode.Equals("serviceprincipal", StringComparison.InvariantCultureIgnoreCase))
            {
                // For app only authentication, we need the specific tenant id in the authority url
                var tenantSpecificUrl = azureAd.Value.AuthorityUrl.Replace("organizations", azureAd.Value.TenantId);

                // Create a confidential client to authorize the app with the AAD app
                IConfidentialClientApplication clientApp = ConfidentialClientApplicationBuilder
                                                                                .Create(azureAd.Value.ClientId)
                                                                                .WithClientSecret(azureAd.Value.ClientSecret)
                                                                                .WithAuthority(tenantSpecificUrl)
                                                                                .Build();
                // Make a client call if Access token is not available in cache
                authenticationResult = await clientApp.AcquireTokenForClient(azureAd.Value.ScopeBase).ExecuteAsync();
            }
            else
            {
                throw new Exception("Invalid Authentication Mode. Please set the AuthenticationMode to either 'MasterUser' or 'ServicePrincipal'.");
            }

            return authenticationResult.AccessToken;
        }
    }
}
