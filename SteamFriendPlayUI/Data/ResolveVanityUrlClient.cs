using SteamFriendPlayUI.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SteamFriendPlayUI.Data
{
    class ResolveVanityUrlClient
    {
        private const string
               URL_BASE =
               @"https://steamfriendplay.azurewebsites.net/api/user/resolveid/";
               //URL_PROPERTIES =
               //@"?vanityUrl={vanityUrl}";

        private static string URL_Properties(string vanityUrl)
            => $"?vanityUrl={vanityUrl}";

        private readonly HttpClient client;

        public ResolveVanityUrlClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(URL_BASE);
            httpClient.AcceptJson();
            this.client = httpClient;
        }

        // vanityUrl can just be the custom name - it does not have to be the entire url.
        public async Task<long?> GetUserIdAsync(string vanityUrl)
        {
            using (HttpResponseMessage response = await RequestAsync(vanityUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var resp = VanityUrlResponse.FromJson(json);
                    return resp.Success ? resp.SteamUserId : null;
                }
            }
            return null;
        }

        private Task<HttpResponseMessage> RequestAsync(string vanityUrl)
            => client.GetAsync(FormatProperties(vanityUrl));

        private string FormatProperties(string vanityUrl)
            => URL_Properties(vanityUrl);
    }
}
