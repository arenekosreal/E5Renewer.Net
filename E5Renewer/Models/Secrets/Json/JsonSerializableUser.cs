using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace E5Renewer.Models.Secrets.Json
{
    internal class JsonSerializableUser
    {
        [JsonIgnore]
        public User value
        {
            get => new(
                this.name, this.tenantId, this.clientId,
                this.secret, this.certificate,
                this.fromTime, this.toTime,
                this.days is not null ? ImmutableList.CreateRange(this.days) : null
            );
        }

        public string name { get; }

        public string tenantId { get; }

        public string clientId { get; }

        public string? secret { get; }

        [JsonConverter(typeof(FileInfoOrNullConverter))]
        public FileInfo? certificate { get; }

        public TimeOnly? fromTime { get; }

        public TimeOnly? toTime { get; }

        public List<DayOfWeek>? days { get; }

        [JsonConstructor]
        public JsonSerializableUser(
            string name, string tenantId, string clientId,
            string? secret, FileInfo? certificate,
            TimeOnly? fromTime, TimeOnly? toTime, List<DayOfWeek>? days
        ) => (
            this.name, this.tenantId, this.clientId,
            this.secret, this.certificate,
            this.fromTime, this.toTime, this.days
        ) = (
            name, tenantId, clientId,
            secret, certificate,
            fromTime, toTime, days
        );
    }
}
