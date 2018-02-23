using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FileWatcherService.Models;

namespace FileWatcherService.Services
{
    public class DummyReceiver : IReceiver
    {
        public void Dispose()
        {           
        }

        public async Task<IEnumerable<Message>> ReceiveMessages(DateTime dateTime, CancellationToken cancellationToken)
        {
            return new[]
            {
                new Message
                {
                    Id =0,
                    ChangedDate = dateTime,
                    Title = "Dummy",
                    Uri = new Uri("http://google.com?q=dummy")
                }
            };
        }
    }
}
