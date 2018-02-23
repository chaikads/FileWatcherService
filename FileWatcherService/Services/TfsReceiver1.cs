using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using System;


namespace FileWatcherService.Services
{
    public class TfsReceiver1 : TfsReceiver
    {
        public TfsReceiver1(WorkItemTrackingHttpClientBase client, IConfiguration configuration) : base(client, configuration)
        {
        }

        protected override string GetQueryString(DateTime dateTime)
        {
            return $"SELECT [System.Id] FROM WorkItems WHERE [System.ChangedDate] < '{ dateTime.ToString("s") }'";
        }
    }
}
