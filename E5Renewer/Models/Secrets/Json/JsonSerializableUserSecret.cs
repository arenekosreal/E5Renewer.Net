using System.Text.Json.Serialization;
using System.Collections.Immutable;

namespace E5Renewer.Models.Secrets.Json
{
    internal class JsonSerializableUserSecret
    {
        [JsonIgnore]
        public UserSecret value
        {
            get => new(
                ImmutableList.CreateRange(this.users.Select((user) => user.value)),
                this.passwords is not null ? ImmutableDictionary.CreateRange(this.passwords) : null
            );
        }
        public List<JsonSerializableUser> users = new();

        public Dictionary<string, string>? passwords;
    }
}
