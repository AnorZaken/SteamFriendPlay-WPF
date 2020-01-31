using Newtonsoft.Json;

namespace SteamFriendPlayUI.Models
{
    public class UserAndFriendsApps
    {
        public static UserAndFriendsApps FromJson(string json)
            => JsonConvert.DeserializeObject<UserAndFriendsApps>(json, JSerialize.Settings);

        [JsonConstructor]
        public UserAndFriendsApps(long steamUserId, UserInfo userInfo, TryIdResponse<UserInfo>[] friends, AppUsers[] apps, bool success)
        {
            SteamUserId = steamUserId;
            UserInfo = userInfo;
            Friends = friends;
            Apps = apps;
            Success = success;
        }

        /// <summary>
        /// Steam user id.
        /// </summary>
        public long SteamUserId { get; }

        /// <summary>
        /// Basic information about the user.
        /// </summary>
        public UserInfo UserInfo { get; }

        /// <summary>
        /// Friends of the user.
        /// </summary>
        /// <remarks>
        /// If the Success property is true, then that friends user info and owned apps was successfully fetched.
        /// </remarks>
        public TryIdResponse<UserInfo>[] Friends { get; }

        /// <summary>
        /// List of Steam apps each including a list of users who owns them.
        /// </summary>
        public AppUsers[] Apps { get; }

        /// <summary>
        /// Successfully fetched the users info, friends list, and owned apps list.
        /// </summary>
        /// <remarks>
        /// Whether or not each friends user info and apps list was fetched successfully
        /// is expressed separately on each friend in the <see cref="Friends"/> list.
        /// </remarks>
        public bool Success { get; }
    }
}
