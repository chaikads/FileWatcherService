
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FileWatcherService.Models;
using FileWatcherService.Services;
using Moq;
using NUnit.Framework;
using RestSharp;
using Slack.Webhooks;

namespace FileWatcherService.Tests.Services
{    
    [TestFixture]
    public class SlackSenderTests
    {
        [Test]
        public void WhenSendMessagesSuccess()
        {
            var restResponceMock = new Mock<IRestResponse>();
            restResponceMock.SetupGet(x => x.ResponseStatus).Returns(ResponseStatus.Completed);

            var slackClientMock = new Mock<ISlackClient>();
            slackClientMock.Setup(x => x.PostAsync(It.IsAny<SlackMessage>())).ReturnsAsync(restResponceMock.Object);

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(x => x.SlackChannel);
            configurationMock.SetupGet(x => x.SlackUserName);

            var sender = new SlackSender(slackClientMock.Object, configurationMock.Object);

            var messages = new[]
            {
                new Message { Title = "Message1" },
                new Message { Title = "Message2" }
            };

            var actual = sender.SendMessages(messages, CancellationToken.None).Result;
            Assert.AreEqual(true, actual);

            slackClientMock.Verify(x => x.PostAsync(It.IsAny<SlackMessage>()), Times.Exactly(2));
        }
    }
}
