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
                var img = new Image();
                img.Source = new BitmapImage(ow.AvatarTiny);
                var marg = img.Margin;
                marg.Left = 2;
                marg.Right = 2;
                img.Margin = marg;
                OwnersList.Items.Add(img);
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
