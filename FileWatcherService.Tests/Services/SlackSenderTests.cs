
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using FileWatcherService.Services;
using NUnit.Framework;
using RestSharp;
using Slack.Webhooks;

namespace FileWatcherService.Tests.Services
{
    public class RestResponseMock : IRestResponse
    {
        public IRestRequest Request { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ContentType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long ContentLength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ContentEncoding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public HttpStatusCode StatusCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string StatusDescription { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte[] RawBytes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Uri ResponseUri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Server { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IList<RestResponseCookie> Cookies => throw new NotImplementedException();

        public IList<Parameter> Headers => throw new NotImplementedException();

        public ResponseStatus ResponseStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Exception ErrorException { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class SlackClientMock : ISlackClient
    {
        public SlackClientMock()
        {
        }

        public bool Post(SlackMessage slackMessage)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IRestResponse> PostAsync(SlackMessage slackMessage)
        {
            return new RestResponseMock();
        }

        public bool PostToChannels(SlackMessage message, IEnumerable<string> channels)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Task<IRestResponse>> PostToChannelsAsync(SlackMessage message, IEnumerable<string> channels)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ConfigurationMock : IConfiguration
    {
        public string SlackChannel => "asd";

        public Uri SlackUri => throw new NotImplementedException();

        public string SlackUserName => "chaika1";

        public string TfsProject => throw new NotImplementedException();

        public string TfsToken => throw new NotImplementedException();

        public Uri TfsUri => throw new NotImplementedException();

        public TimeSpan Timeout => throw new NotImplementedException();

        public string EventLogSource => throw new NotImplementedException();

        public string EventLogName => throw new NotImplementedException();

        public EventLogEntryType EventLogEntryType => throw new NotImplementedException();  
    }

    [TestFixture]
    public class SlackSenderTests
    {
        [Test]
        public void WhenSendMessagesSuccess()
        {
            var configuration = new ConfigurationMock();
            var slackClient = new SlackClientMock();
            var sender = new SlackSender(slackClient, configuration);

        }
    }
}
