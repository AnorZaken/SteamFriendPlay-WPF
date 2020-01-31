namespace SteamFriendPlayUI.Data
{
    public class FriendPlayRequestParameters
    {
        public FriendPlayRequestParameters(
            long steamUserId,
            int ownershipThreshold = 3,
            bool onlyUserOwned = true,
            bool includePlayedFreeGames = true)
        {
            SteamUserId = steamUserId;
            OwnershipThreshold = ownershipThreshold;
            OnlyUserOwned = onlyUserOwned;
            IncludePlayedFreeGames = includePlayedFreeGames;
        }

        public long SteamUserId { get; }
        public int OwnershipThreshold { get; }
        public bool OnlyUserOwned { get; }
        public bool IncludePlayedFreeGames { get; }
    }
}
