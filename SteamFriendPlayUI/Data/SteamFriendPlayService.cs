using SteamFriendPlayUI.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SteamFriendPlayUI.Data
{
    class SteamFriendPlayService
    {
        private readonly ResolveVanityUrlClient rvClient;
        private readonly FriendPlayClient fpClient;

        public SteamFriendPlayService()
        {
            this.rvClient = new ResolveVanityUrlClient(new HttpClient());
            this.fpClient = new FriendPlayClient(new HttpClient());
        }

        public Task<long?> ResolveVanityUrlAsync(string vanityUrl)
            => rvClient.GetUserIdAsync(vanityUrl);

        public Task<UserAndFriendsApps> GetUserAndFriendsAppsAsync(long steamUserId)
            => fpClient.GetUserAndFriendsAppsAsync(steamUserId);
    }
}
