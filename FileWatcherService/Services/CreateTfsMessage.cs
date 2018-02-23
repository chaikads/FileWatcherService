using FileWatcherService.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcherService.Services
{
    public class CreateTfsMessage : ICreateTfsMessage
    {        
        private readonly IConfiguration configuration;
        

        public CreateTfsMessage(IConfiguration configuration)
        {
            this.configuration = configuration;         
        }

        public  Message CreateMessage(WorkItem workItem)
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
