namespace E5Renewer.Models.Config
{
    /// <summary>class to store info to access msgraph apis.</summary>
    /// <remarks>For compatibility to Python version.</remarks>
    public sealed record class GraphUser : ICheckable
    {
        /// <value>The name of user.</value>
        public string name { get; set; }

        /// <value>The tenant id of user.</value>
        public string tenantId { get; set; }

        /// <value>The client id of user.</value>
        public string clientId { get; set; }

        /// <value>The secret of user.</value>
        public string secret { get; set; }

        /// <value>The path to certificate of user.</value>
        public string certificate { get; set; }

        /// <value>When to start the user.</value>
        public TimeOnly fromTime { get; set; }

        /// <value>When to stop the user.</value>
        public TimeOnly toTime { get; set; }

        /// <value>Which days are allowed to start the user.</value>
        public List<DayOfWeek> days { get; set; }

        /// <value>When to start the user.</value>
        public bool isCheckPassed
        {
            get
            {
                string[] items = [this.name, this.tenantId, this.clientId];
                return items.All((item) => !string.IsNullOrEmpty(item)) && this.IsCertificateOrSecretValid();
            }
        }

        /// <value>If the user is allowed to start.</value>
        public bool enabled
        {
            get
            {
                DateTime now = DateTime.Now;
                DateOnly today = DateOnly.FromDateTime(now);
                DateTime fromTime = new(today, this.fromTime);
                DateTime toTime = new(today, this.toTime);
                while (fromTime >= toTime)
                {
                    toTime = toTime.AddDays(1);
                }
                return now >= fromTime &&
                    now < toTime &&
                    this.days.Contains(now.DayOfWeek);
            }
        }

        /// <value>How long to start the user.</value>
        public TimeSpan timeToStart
        {
            get
            {
                if (this.enabled)
                {
                    return new();
                }
                DateTime now = DateTime.Now;
                DateOnly today = DateOnly.FromDateTime(now);
                DateTime fromTime = new(today, this.fromTime);
                DateTime toTime = new(today, this.toTime);
                while (fromTime >= toTime)
                {
                    toTime = toTime.AddDays(1);
                }
                if (now < fromTime)
                {
                    return fromTime - now;
                }
                if (now >= toTime)
                {
                    return this.GetSuitableDateTime(
                        now,
                        (item) => now < item,
                        (date) =>
                            new(
                                DateOnly.FromDateTime(date).AddDays(1),
                                this.fromTime
                            )
                    ) - now;
                }
                // this.days do not contains today
                return this.GetSuitableDateTime(
                    now,
                    (item) => this.days.Contains(item.DayOfWeek),
                    (date) =>
                        new(
                            DateOnly.FromDateTime(date).AddDays(1),
                            this.fromTime
                        )
                ) - now;
            }
        }

        /// <summary>
        /// Initialize <c>GraphUser</c> with default values.
        /// </summary>
        public GraphUser()
        {
            this.name = "";
            this.tenantId = "";
            this.clientId = "";
            this.secret = "";
            this.certificate = "";
            this.fromTime = new(8, 0);
            this.toTime = new(16, 0);
            this.days = new()
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                DayOfWeek.Thursday, DayOfWeek.Friday
            };
        }

        private DateTime GetSuitableDateTime(DateTime start, Predicate<DateTime> passCondition, Func<DateTime, DateTime> genNextItem)
        {
            while (!passCondition(start))
            {
                start = genNextItem(start);
            }
            return start;
        }

        private bool IsCertificateOrSecretValid()
        {
            return File.Exists(this.certificate) || !string.IsNullOrEmpty(this.secret);
        }
    }
}
