using SteamFriendPlayUI.Models;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SteamFriendPlayUI.Controls
{
    public sealed partial class SteamApplistItemControl : UserControl
    {
        private string _appName;
        private UserInfo[] _owners;

        public string AppName
        {
            get => _appName;
            set => UpdateName(value);
        }

        public UserInfo[] Owners
        {
            get => _owners;
            set => UpdateOwners(value);
        }

        private void UpdateOwners(UserInfo[] owners)
        {
            _owners = owners;
            OwnersList.Items.Clear();
            foreach (var ow in owners)
            {
                OwnersList.Items.Add(new GameOwnerControl(ow.AvatarTinyBmp, ow.Name));
            }
        }

        private void UpdateName(string appName)
        {
            _appName = appName;
            AppName_TextBlock.Text = appName;
        }

        public SteamApplistItemControl()
        {
            this.InitializeComponent();
        }
    }
}
