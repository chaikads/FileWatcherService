using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileWatcherService.Models;
using RestSharp;
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

        public async Task<bool> SendMessages(IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            //var result = true;

            //foreach (var item in messages)
            //{
            //    result = result || await this.SendMessage(item);               
            //}

            //return result;
            // return await messages.Aggregate(Task.FromResult(true), async (r, x) => await r || await this.SendMessage(x));
            var tasks = messages.Select(this.SendMessage);
            var results = await Task.WhenAll(tasks);
            return results.All(x => x);
        }

        private async Task<bool> SendMessage(Message message)
        {          
            var slackMessage = new SlackMessage
            {
                Channel = this.configuration.SlackChannel,
                Username = this.configuration.SlackUserName,
                IconEmoji = ":email:",
                Text = $"Отловленное событие: {message.Title} {message.ChangedDate} {message.Uri}"
            };

            
            var response = await client.PostAsync(slackMessage);
            return response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}
