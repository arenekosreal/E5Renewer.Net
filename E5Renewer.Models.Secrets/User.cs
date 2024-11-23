using System.Collections.Immutable;

namespace E5Renewer.Models.Secrets;

/// <summary>Readonly struct to store user secret.</summary>
public readonly struct User
{
    /// <value>The name of user.</value>
    public string name { get; }

    /// <value>The tenant id of user.</value>
    public string tenantId { get; }

    /// <value>The client id of user.</value>
    public string clientId { get; }

    /// <value>The secret of user.</value>
    public string? secret { get; }

    /// <value>The path to certificate of user.</value>
    public FileInfo? certificate { get; }

    /// <value>When to start the user.</value>
    private TimeOnly? fromTime { get; }

    /// <value>When to stop the user.</value>
    private TimeOnly? toTime { get; }

    /// <value>Which days are allowed to start the user.</value>
    private ImmutableList<DayOfWeek>? days { get; }

    /// <value>If this secret is valid to be used.</value>
    public bool valid
    {
        get
        {
            string[] nonEmptyItems = [this.name, this.tenantId, this.clientId];
            return nonEmptyItems.All((i) => !string.IsNullOrEmpty(i)) &&
                (this.certificate?.Exists ?? false || !string.IsNullOrEmpty(this.secret));
        }
    }

    /// <value>How long to start the user.</value>
    public TimeSpan timeToStart
    {
        get
        {
            TimeOnly fromTime = this.fromTime ?? new(8, 0);
            TimeOnly toTime = this.toTime ?? new(16, 0);
            ImmutableList<DayOfWeek> days = this.days ?? ImmutableList.Create(
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);

            DateTime now = DateTime.Now;
            DateOnly today = DateOnly.FromDateTime(now);
            DateTime fromDateTime = new(today, fromTime);
            DateTime toDateTime = new(today, toTime);
            while (fromDateTime >= toDateTime)
            {
                toDateTime = toDateTime.AddDays(1);
            }
            if (now <= fromDateTime)
            {
                DateTime nextFromTime = fromDateTime;
                while (!days.Contains(nextFromTime.DayOfWeek))
                {
                    nextFromTime = nextFromTime.AddDays(1);
                }
                return nextFromTime - now;
            }
            else
            {
                DateTime nextFromDateTime = now;
                DateTime nextToDateTime = toDateTime;
                while (!days.Contains(nextFromDateTime.DayOfWeek) || nextFromDateTime >= nextToDateTime)
                {
                    DateOnly startDate = DateOnly.FromDateTime(nextFromDateTime).AddDays(1);
                    nextFromDateTime = new(startDate, fromTime);
                    nextToDateTime = nextToDateTime.AddDays(1);
                }
                return nextFromDateTime - now;
            }
        }
    }

    /// <summary>Initialize <see cref="User"/> instance with arguments given.</summary>
    public User(
        string name, string tenantId, string clientId, string? secret, FileInfo? certificate,
        TimeOnly? fromTime, TimeOnly? toTime, ImmutableList<DayOfWeek>? days
    ) => (this.name, this.tenantId, this.clientId, this.secret, this.certificate, this.fromTime, this.toTime, this.days) =
        (name, tenantId, clientId, secret, certificate, fromTime, toTime, days);

}
