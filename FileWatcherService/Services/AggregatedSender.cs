using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileWatcherService.Models;

namespace FileWatcherService.Services
{
    public class AggregatedSender : ISender
    {
        private readonly ISender[] senders;

        public AggregatedSender(params ISender[] senders)
        {
            this.senders = senders;
        }

        public void Dispose()
        {
            this.senders.ToList().ForEach(x => x.Dispose());
        }

        public async Task<bool> SendMessages(IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            var tasks = this.senders.Select(x => x.SendMessages(messages, cancellationToken));
            var results = await Task.WhenAll(tasks);
            return results.All(x => x);
        }
    }
}
