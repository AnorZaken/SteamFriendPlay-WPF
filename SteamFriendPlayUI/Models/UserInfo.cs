using System;

namespace SteamFriendPlayUI.Models
{
    public class UserInfo
    {
        public string Name { get; set; }
        public Uri AvatarFull { get; set; }
        public Uri AvatarSmall { get; set; }
        public Uri AvatarTiny { get; set; }
        public Uri ProfileUrl { get; set; }
        public bool IsPublic { get; set; }
    }
}
