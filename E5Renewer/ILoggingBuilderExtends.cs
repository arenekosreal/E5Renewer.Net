namespace E5Renewer
{
    internal static class ILoggingBuilderExtends
    {
        public static ILoggingBuilder AddConsole(this ILoggingBuilder builder, bool systemd, LogLevel level)
        {
            const string timeStampFormat = "yyyy-MM-dd HH:mm:ss ";

            builder.ClearProviders();
            if (systemd)
            {
                builder.AddSystemdConsole((config) => config.TimestampFormat = timeStampFormat);
            }
            else
            {
                builder.AddSimpleConsole(
                    (config) =>
                    {
                        config.SingleLine = true;
                        config.TimestampFormat = timeStampFormat;
                    }
                );
            }
            builder.SetMinimumLevel(level);
            return builder;
        }
    }
}
