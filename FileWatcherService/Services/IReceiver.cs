using FileWatcherService.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FileWatcherService.Services
{
    public interface IReceiver : IDisposable
    {
        Task<IEnumerable<Message>> ReceiveMessages(DateTime dateTime, CancellationToken cancellationToken);
    }
}
