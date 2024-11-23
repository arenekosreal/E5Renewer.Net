using System.Collections.Immutable;

using VYaml.Annotations;

namespace E5Renewer.Models.Secrets.Yaml
{
    [YamlObject(NamingConvention.SnakeCase)]
    internal partial class YamlSerializeCompatibleUserSecret
    {
        [YamlIgnore]
        public UserSecret value
        {
            get => new(
                ImmutableList.CreateRange(users.Select((user) => user.value)),
                passwords is not null ? ImmutableDictionary.CreateRange(passwords) : null
            );
        }

        public List<YamlSerializeCompatibleUser> users { get; }

        public Dictionary<string, string>? passwords { get; }

        [YamlConstructor]
        public YamlSerializeCompatibleUserSecret(List<YamlSerializeCompatibleUser> users, Dictionary<string, string>? passwords) =>
            (this.users, this.passwords) = (users, passwords);
    }
}
