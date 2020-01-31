using System.Net.Http;

namespace SteamFriendPlayUI
{
    static class HttpClientExtensions
    {
        public static void SetUserAgent(this HttpClient client, string userAgent)
            => client.DefaultRequestHeaders.Add("User-Agent", userAgent);

        public static void AcceptJson(this HttpClient client)
            => client.DefaultRequestHeaders.Add("Accept", "application/json");
    }
}
