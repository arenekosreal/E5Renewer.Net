namespace E5Renewer.Config
{
    public class GraphUser : ICheckable
    {
        private const uint calmDownMinMilliSeconds = 300000;
        private const uint calmDownMaxMilliSeconds = 500000;
        private readonly TimeOnly[] privateTimes = new TimeOnly[2];
        public string name { get; set; }
        public string tenantId { get; set; }
        public string clientId { get; set; }
        public string secret { get; set; }
        public TimeOnly fromTime
        {
            get
            {
                return privateTimes[0];
            }
            set
            {
                if (value < this.toTime)
                {
                    privateTimes[0] = value;
                }
            }
        }
        public TimeOnly toTime
        {
            get
            {
                return privateTimes[1];
            }
            set
            {
                if (value > this.fromTime)
                {
                    privateTimes[1] = value;
                }
            }
        }
        public List<DayOfWeek> days { get; set; }
        public bool enabled
        {
            get
            {
                DateTime today = DateTime.Today;
                DateTime fromTime = new(
                    DateOnly.FromDateTime(today),
                    this.fromTime,
                    DateTimeKind.Local
                );
                DateTime toTime = new(
                    DateOnly.FromDateTime(today),
                    this.toTime,
                    DateTimeKind.Local
                );
                DateTime now = DateTime.Now;
                return days.Contains(now.DayOfWeek) &&
                    now.CompareTo(fromTime) >= 0 &&
                    now.CompareTo(toTime) < 0;
            }
        }
        public bool check
        {
            get
            {
                return this.name != "" &&
                    this.tenantId != "" &&
                    this.clientId != "" &&
                    this.secret != "";
            }
        }
        public int milliSecondsUntilNextStarting
        {
            get
            {
                DateTime now = DateTime.Now;
                DateTime today = DateTime.Today;
                DateTime fromTime = new(
                    DateOnly.FromDateTime(today),
                    this.fromTime,
                    DateTimeKind.Local
                );
                DateTime toTime = new(
                    DateOnly.FromDateTime(today),
                    this.toTime,
                    DateTimeKind.Local
                );
                if (now.CompareTo(fromTime) < 0) // Wait until starts
                {
                    return (fromTime - now).Milliseconds;
                }
                if (now.CompareTo(toTime) >= 0) // Wait until tomorrow
                {
                    TimeSpan toEndOfDay = DateTime.MaxValue - now;
                    TimeSpan toStartTimeOfNextDay = fromTime - DateTime.MinValue;
                    return (toEndOfDay + toStartTimeOfNextDay).Milliseconds;
                }
                // Wait until calm down is reached
                Random random = new();
                return random.Next(
                    (int)calmDownMinMilliSeconds,
                    (int)calmDownMaxMilliSeconds);

            }
        }
        public GraphUser()
        {
            this.name = "";
            this.tenantId = "";
            this.clientId = "";
            this.secret = "";
            this.fromTime = new(8, 0);
            this.toTime = new(16, 0);
            this.days = new()
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                DayOfWeek.Thursday, DayOfWeek.Friday
            };
        }
    }
}
