using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FileWatcherService.Models;
using Slack.Webhooks;

namespace FileWatcherService.Services
{
    public class SlackSender : ISender
    {
        private readonly ISlackClient client;
        private readonly IConfiguration configuration;
        public SlackSender(ISlackClient client, IConfiguration configuration)
        {
            this.client = client;
            this.configuration = configuration;
        }
        public void Dispose()
        {           
        }

        public async Task SendMessages(IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            foreach (var item in messages)
            {
                await this.SendMessage(item);
            }
        }

        private async Task SendMessage(Message message)
        {          
            var slackMessage = new SlackMessage
            {
                Channel = this.configuration.SlackChannel,
                Username = this.configuration.SlackUserName,
                IconEmoji = ":email:",
                Text = $"Отловленное событие: {message.Title} {message.ChangedDate} {message.Uri}"
            };

            await client.PostAsync(slackMessage);
        }
    }
}
