using SteamFriendPlayUI.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SteamFriendPlayUI.Data
{
    class FriendPlayClient
    {
        private const string
               URL_BASE =
               @"https://steamfriendplay.azurewebsites.net/api/play/";
               //URL_PROPERTIES =
               //@"?userId={long}&ownershipThreshold={int}&onlyUserOwned={bool}&includePlayedFreeGames={bool}";
               
        private static string URL_Properties(FriendPlayRequestParameters rp)
            => $"?userId={rp.SteamUserId}&ownershipThreshold={rp.OwnershipThreshold}&onlyUserOwned={rp.OnlyUserOwned}&includePlayedFreeGames={rp.IncludePlayedFreeGames}";

        private readonly HttpClient client;

        public FriendPlayClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(URL_BASE);
            httpClient.AcceptJson();
            this.client = httpClient;
        }

        public Task<UserAndFriendsApps> GetUserAndFriendsAppsAsync(long steamUserId)
            => GetUserAndFriendsAppsAsync(new FriendPlayRequestParameters(steamUserId));

        public async Task<UserAndFriendsApps> GetUserAndFriendsAppsAsync(FriendPlayRequestParameters requestParameters)
        {
            using (HttpResponseMessage response = await RequestAsync(requestParameters))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var resp = UserAndFriendsApps.FromJson(json);
                    return resp?.Success == true ? resp : null;
                }
            }
            return null;
        }

        private Task<HttpResponseMessage> RequestAsync(FriendPlayRequestParameters rp)
            => client.GetAsync(FormatProperties(rp));

        private string FormatProperties(FriendPlayRequestParameters rp)
            => URL_Properties(rp);
    }
}
