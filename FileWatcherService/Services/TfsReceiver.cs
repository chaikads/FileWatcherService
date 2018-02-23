using FileWatcherService.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileWatcherService.Services
{
    public abstract class TfsReceiver : IReceiver
    {
        private readonly WorkItemTrackingHttpClientBase client;
        private readonly IConfiguration configuration;
       
        protected TfsReceiver(WorkItemTrackingHttpClientBase client, IConfiguration configuration)
        {
            this.client = client;
            this.configuration = configuration;
        }

        protected abstract string GetQueryString(DateTime dateTime);

        public void Dispose()
        {
            this.client.Dispose();
        }

        public async Task<IEnumerable<Message>> ReceiveMessages(DateTime dateTime, CancellationToken cancellationToken)
        {
            var wiql = new Wiql { Query = this.GetQueryString(dateTime) };
            var items = await this.client.QueryByWiqlAsync(wiql, this.configuration.TfsProject, true, cancellationToken: cancellationToken);

            if (!items.WorkItems.Any())
            {
                return Enumerable.Empty<Message>();
            }

            var ids = items.WorkItems.Select(x => x.Id);
            var entries = await client.GetWorkItemsAsync(ids, cancellationToken: cancellationToken);
            return entries.Select(this.CreateMessage);
        }
        private Message CreateMessage(WorkItem workItem)
        {
            return new Message
            {
                Id = workItem.Id,
                Title = (string)workItem.Fields["System.Title"],
                Uri = new Uri(this.configuration.TfsUri, $"/{this.configuration.TfsProject}/_workitems/edit/{workItem.Id}"),
                ChangedDate = (DateTime)workItem.Fields["System.ChangedDate"]
            };
        }
    }
}
