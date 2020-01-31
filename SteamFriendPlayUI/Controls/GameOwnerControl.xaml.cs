using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SteamFriendPlayUI.Controls
{
    public sealed partial class GameOwnerControl : UserControl
    {
        public GameOwnerControl(BitmapImage avatar, string name)
        {
            this.InitializeComponent();
            if (avatar != null)
                Avatar.Source = avatar;
            ToolTip.Text = name;
        }
    }
}
