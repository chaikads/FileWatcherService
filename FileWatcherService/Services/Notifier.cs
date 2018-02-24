using FileWatcherService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileWatcherService.Services
{
    public class Notifier : INotifier
    {
        private readonly IReceiver receiver;
        private readonly ISender sender;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;        

        public Notifier(IReceiver receiver, ISender sender, ILogger logger, IConfiguration configuration)
        {
            this.receiver = receiver;
            this.sender = sender;
            this.logger = logger;
            this.configuration = configuration;
        }

        public void Dispose()
        {
            this.receiver.Dispose();
            this.sender.Dispose();
        }

        public async void Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var dateTime = DateTime.UtcNow;
                try
                {
                    var messages = await this.receiver.ReceiveMessages(dateTime, cancellationToken);
                    var result = await this.sender.SendMessages(messages, cancellationToken);
                    this.logger.Log(result ? "Сообщения посланы" : "Неудача");
                }
                catch (Exception e)
                {
                    this.logger.Log(e.Message);
                }
                
                await Task.Delay(this.configuration.Timeout, cancellationToken);
            }
        }                
    }
}
