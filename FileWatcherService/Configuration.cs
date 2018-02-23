using System;
using System.Configuration;
using System.Diagnostics;

namespace FileWatcherService
{
    public class Configuration : IConfiguration
    {
        public Uri TfsUri => new Uri(ConfigurationManager.AppSettings["TfsUri"]);

        public string TfsToken => ConfigurationManager.AppSettings["TfsToken"];

        public string TfsProject => ConfigurationManager.AppSettings["TfsProject"];

        public TimeSpan Timeout => TimeSpan.Parse(ConfigurationManager.AppSettings["Timeout"]);

        public Uri SlackUri => new Uri(ConfigurationManager.AppSettings["SlackUri"]);

        public string SlackChannel => ConfigurationManager.AppSettings["SlackChannel"];

        public string SlackUserName => ConfigurationManager.AppSettings["SlackUserName"];      

        public string EventLogName => ConfigurationManager.AppSettings["EventLogName"];

        public EventLogEntryType EventLogEntryType => (EventLogEntryType)Enum.Parse(typeof(EventLogEntryType), ConfigurationManager.AppSettings["EventLogEntryType"]);

        public string EventLogSource => ConfigurationManager.AppSettings["EventLogSource"];
    }
}
