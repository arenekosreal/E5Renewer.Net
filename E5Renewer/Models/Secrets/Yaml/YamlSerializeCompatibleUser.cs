using System.Collections.Immutable;

using VYaml.Annotations;

namespace E5Renewer.Models.Secrets.Yaml
{
    [YamlObject(NamingConvention.SnakeCase)]
    internal partial class YamlSerializeCompatibleUser
    {
        [YamlIgnore]
        public User value
        {
            get
            {
                FileInfo? certificate = this.certificate is not null ? new(this.certificate) : null;
                ImmutableList<DayOfWeek>? days =
                    this.days is not null
                        ? ImmutableList.CreateRange(this.days.Select((day) => (DayOfWeek)day))
                        : null;
                return new(
                    this.name, this.tenantId, this.clientId, this.secret, certificate,
                    this.fromTime, this.toTime, days
                );
            }
        }

        public string name { get; }

        public string tenantId { get; }

        public string clientId { get; }

        public string? secret { get; }

        public string? certificate { get; }

        public TimeOnly? fromTime { get; }

        public TimeOnly? toTime { get; }

        public List<int>? days { get; }

        [YamlConstructor]
        public YamlSerializeCompatibleUser(
            string name, string tenantId, string clientId,
            string? secret, string? certificate, TimeOnly? fromTime, TimeOnly? toTime, List<int>? days
        ) => (this.name, this.tenantId, this.clientId, this.secret, this.certificate, this.fromTime, this.toTime, this.days) =
                (name, tenantId, clientId, secret, certificate, fromTime, toTime, days);
    }
}
