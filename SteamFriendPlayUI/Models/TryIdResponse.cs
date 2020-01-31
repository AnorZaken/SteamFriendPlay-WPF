using Newtonsoft.Json;

namespace SteamFriendPlayUI.Models
{
    public readonly struct TryIdResponse<T>
    {
        [JsonConstructor]
        public TryIdResponse(bool success, long id, T value)
        {
            Success = success;
            Id = id;
            Value = value;
        }

        public bool Success { get; }
        public long Id { get; }
        public T Value { get; }

        public static TryIdResponse<T> Failure(long id) => new TryIdResponse<T>(id);

        private TryIdResponse(long id)
        {
            Success = false;
            Id = id;
            Value = default;
        }
    }
}
