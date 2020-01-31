using SteamFriendPlayUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SteamFriendPlayUI.Controls
{
    public sealed partial class UserInfoControl : UserControl
    {
        private UserInfo _userInfo;

        public UserInfo UserInfo
        {
            get => _userInfo;
            private set => UpdateUserInfo(value);
        }

        public long SteamUserId { get; private set; }

        public TryIdResponse<UserInfo> Data {
            set { SteamUserId = value.Id; UserInfo = value.Value; }
        }

        private void UpdateUserInfo(UserInfo userInfo)
        {
            _userInfo = userInfo;
            DisplayName.Text = userInfo.Name;
            if (userInfo.AvatarSmallBmp != null)
                Avatar.Source = userInfo.AvatarSmallBmp;
            ToolTip.Text = userInfo.ProfileUrl.ToString();
        }

        public UserInfoControl()
        {
            this.InitializeComponent();
        }
    }
}
