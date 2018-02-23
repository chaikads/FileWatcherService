using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileWatcherService.Models;

namespace FileWatcherService.Services
{
    public class AggregatedReceiver : IReceiver
    {
        private readonly IReceiver[] receivers;

        public AggregatedReceiver(params IReceiver[] receivers)
        {
            this.receivers = receivers;
        }

        public void Dispose()
        {
            this.receivers.ToList().ForEach(x => x.Dispose());
        }

        public async Task<IEnumerable<Message>> ReceiveMessages(DateTime dateTime, CancellationToken cancellationToken)
        {
            var tasks = this.receivers.Select(x => x.ReceiveMessages(dateTime, cancellationToken));
            var results = await Task.WhenAll(tasks);
            return results.SelectMany(x => x);
        }
    }
}
