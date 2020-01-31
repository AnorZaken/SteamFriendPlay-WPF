using Newtonsoft.Json;

namespace SteamFriendPlayUI.Models
{
    public class VanityUrlResponse
    {
        public static VanityUrlResponse FromJson(string json)
            => JsonConvert.DeserializeObject<VanityUrlResponse>(json, JSerialize.Settings);

        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        public long? SteamUserId { get; set; }
    }
}
