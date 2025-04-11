using AppOwnsData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AppOwnsData.Controllers
{
    public class TestsController(IOptions<AzureAd> azureAd) : Controller
    {

        [HttpPost]
        public IActionResult ServicePrincipalAuthTest()
        {
            string tenantId = azureAd.Value.TenantId;
            string url = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
            StringContent content = new StringContent($"client_id={azureAd.Value.ClientId}&client_secret={azureAd.Value.ClientSecret}&scope={azureAd.Value.ScopeBase[0]}&grant_type=client_credentials", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
            message.Content = content;
            HttpResponseMessage response = client.Send(message);
            string responseBody = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest("Service Principal authentication failed.");
            }
            if (string.IsNullOrEmpty(responseBody))
            {
                return BadRequest("Service Principal authentication failed.");
            }
            return Content(responseBody, "application/json");
        }
    }
}
