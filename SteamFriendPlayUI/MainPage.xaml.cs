using SteamFriendPlayUI.Controls;
using SteamFriendPlayUI.Data;
using SteamFriendPlayUI.Models;
using SteamFriendPlayUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SteamFriendPlayUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string PLEASE_WAIT = "Please wait...";
        private const string NOT_FOUND = "Not Found!";
        
        private readonly SteamFriendPlayService _fpService;
        private string _notFoundVanity = null;
        private long? _steamId = null;

        public MainPage()
        {
            this.InitializeComponent();
            FriendPlay = new FriendPlayViewModel();
            _fpService = new SteamFriendPlayService();
        }

        public FriendPlayViewModel FriendPlay { get; set; }

        private async void ResolveButton_Click(object sender, RoutedEventArgs e)
        {
            ResolveButton.IsEnabled = false;
            VanityUrl_TextBox.IsEnabled = false; // <-- prevents us from typing in new values until the current search if resolved.
            var vanityUrl = VanityUrl_TextBox.Text;
            _notFoundVanity = vanityUrl; // <-- prevents us from from searching again until the current search is resolved.
            if (!string.IsNullOrWhiteSpace(vanityUrl))
            {
                SteamUserId_TextBox.IsEnabled = false;
                SteamUserId_TextBox.Text = PLEASE_WAIT;
                long? response = null;
                string failText = NOT_FOUND;
                try
                {
                    response = await _fpService.ResolveVanityUrlAsync(vanityUrl);
                }
                catch (HttpRequestException)
                {
                    failText = "Error! :(";
                }
                if (response is long userId)
                {
                    SteamUserId_TextBox.Text = userId.ToString();
                    _notFoundVanity = null;
                    ResolveButton.IsEnabled = false;
                    await FocusManager.TryFocusAsync(SteamUserId_TextBox, FocusState.Programmatic);
                    //PlayButton.Focus(FocusState.Programmatic);
                }
                else
                {
                    SteamUserId_TextBox.Text = failText;
                    //VanityUrl.Focus(FocusState.Programmatic);
                }
            }
            SteamUserId_TextBox.IsEnabled = true;
            VanityUrl_TextBox.IsEnabled = true;
        }

        private void VanityUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            TryEnableResolveButton();
        }

        private void SteamUserId_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = SteamUserId_TextBox.Text;
            if (text != null && long.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out long id))
            {
                _steamId = id;
                PlayButton.IsEnabled = true;
            }
            else
            {
                PlayButton.IsEnabled = false;
                _steamId = null;
            }
            TryEnableResolveButton();
        }

        private void TryEnableResolveButton()
        {
            var text = VanityUrl_TextBox.Text;
            ResolveButton.IsEnabled = !string.IsNullOrWhiteSpace(text) && text != _notFoundVanity && SteamUserId_TextBox.Text != PLEASE_WAIT;
        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.IsEnabled = false;
            SteamUserId_TextBox.IsEnabled = false;
            VanityUrl_TextBox.IsEnabled = false;
            if (_steamId is long id)
            {
                var text = SteamUserId_TextBox.Text;
                SteamUserId_TextBox.Text = PLEASE_WAIT;
                FriendPlay.Data = null;
                var result = await _fpService.GetUserAndFriendsAppsAsync(id);
                UpdatePlayData(result);
                SteamUserId_TextBox.Text = text;
            }
            SteamUserId_TextBox.IsEnabled = true;
            VanityUrl_TextBox.IsEnabled = true;
        }

        private void UpdatePlayData(UserAndFriendsApps playData)
        {
            FriendPlay.Data = playData;
            FriendsList.Items.Clear();
            GamesList.Items.Clear();
            if (playData == null)
            {
                //FriendsList.Items.Add("Load up a userId above!");
            }
            else
            {
                FriendsList.Items.Add(new UserInfoControl() { Data = new TryIdResponse<UserInfo>(true, playData.SteamUserId, playData.UserInfo) });
                foreach (var user in playData.Friends)
                {
                    if (user.Success)
                    {
                        var ui = new UserInfoControl() { Data = user };
                        FriendsList.Items.Add(ui);
                    }
                }

                var dict = playData.Friends.Where(ui => ui.Success).ToDictionary(ui => ui.Id, ui => ui.Value);

                foreach (var app in playData.Apps)
                {
                    GamesList.Items.Add(new SteamApplistItemControl()
                    {
                        AppName = app.AppName,
                        Owners = app.UserIds.Select(id => (dict.TryGetValue(id, out var ui), ui)).Where(t => t.Item1).Select(t => t.Item2).ToArray(),
                    });
                }
            }
        }

        private void VanityUrl_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && ResolveButton.IsEnabled)
                ResolveButton_Click(sender, e);
        }

        private void SteamUserId_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && PlayButton.IsEnabled)
                PlayButton_Click(sender, e);
        }
    }
}
