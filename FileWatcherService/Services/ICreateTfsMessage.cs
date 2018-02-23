using FileWatcherService.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace FileWatcherService.Services
{
    public interface ICreateTfsMessage
    {
        Message CreateMessage(WorkItem workItem);
    }
}