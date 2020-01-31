using Newtonsoft.Json;
using System;
using Windows.UI.Xaml.Media.Imaging;

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

        [JsonIgnore]
        public BitmapImage AvatarFullBmp => (avFull ?? (AvatarFull == null ? null : (avFull = new BitmapImage(AvatarFull))));
        [JsonIgnore]
        public BitmapImage AvatarSmallBmp => (avSmall ?? (AvatarSmall == null ? null : (avSmall = new BitmapImage(AvatarSmall))));
        [JsonIgnore]
        public BitmapImage AvatarTinyBmp => (avTiny ?? (AvatarTiny == null ? null : (avTiny = new BitmapImage(AvatarTiny))));

        private BitmapImage avFull, avSmall, avTiny;
    }
}
