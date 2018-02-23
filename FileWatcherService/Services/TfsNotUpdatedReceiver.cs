using System;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace FileWatcherService.Services
{
    public class TfsNotUpdatedReceiver : TfsReceiver
    {
        public TfsNotUpdatedReceiver(WorkItemTrackingHttpClientBase client, IConfiguration configuration) : base(client, configuration)
        {
        }

        protected override string GetQueryString(DateTime dateTime)
        {
            return $"SELECT [System.Id] FROM WorkItems WHERE [System.ChangedDate] < '{ dateTime.ToString("s") }' ORDER BY [System.State] ASC, [System.ChangedDate] DESC";
        }
    }
}

