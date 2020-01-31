using Newtonsoft.Json;

namespace SteamFriendPlayUI.Models
{
    public class AppUsers
    {
        [JsonConstructor]
        public AppUsers(long appId, string appName, long[] userIds)
        {
            AppId = appId;
            AppName = appName;
            UserIds = userIds;
        }

        public long AppId { get; }
        public string AppName { get; }
        public long[] UserIds { get; }
    }
}
