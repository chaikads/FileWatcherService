using FileWatcherService.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FileWatcherService.Services
{
    public interface ISender : IDisposable
    {
        Task<bool> SendMessages(IEnumerable<Message> messages, CancellationToken cancellationToken);
    }
}