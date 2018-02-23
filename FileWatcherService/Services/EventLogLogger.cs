using System.Diagnostics;

namespace FileWatcherService.Services
{
    public class EventLogLogger : ILogger
    {
        private readonly IConfiguration configuration;

        public EventLogLogger(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Log(string message)
        {
            if (!EventLog.SourceExists(this.configuration.EventLogSource))
            {
                EventLog.CreateEventSource(this.configuration.EventLogSource, this.configuration.EventLogName);
            }

            EventLog.WriteEntry(this.configuration.EventLogSource, message, this.configuration.EventLogEntryType);
        }
    }
}
