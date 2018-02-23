using System;
using System.Diagnostics;

namespace FileWatcherService
{
    public interface IConfiguration
    {
        string SlackChannel { get; }
        Uri SlackUri { get; }
        string SlackUserName { get; }
        string TfsProject { get; }
        string TfsToken { get; }
        Uri TfsUri { get; }
        TimeSpan Timeout { get; }
        string EventLogSource { get; }
        string EventLogName { get; }
        EventLogEntryType EventLogEntryType { get; }
    }
}