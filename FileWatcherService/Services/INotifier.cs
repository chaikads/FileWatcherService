using System;
using System.Threading;

namespace FileWatcherService.Services
{
    public interface INotifier : IDisposable
    {
        void Run(CancellationToken cancellationToken);
    }
}