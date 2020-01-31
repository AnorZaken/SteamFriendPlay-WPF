using SteamFriendPlayUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamFriendPlayUI.ViewModels
{
    public class FriendPlayViewModel : NotificationBase
    {
        private UserAndFriendsApps _data;

        public FriendPlayViewModel()
        {
            //
        }

        public UserAndFriendsApps Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }
    }
}
