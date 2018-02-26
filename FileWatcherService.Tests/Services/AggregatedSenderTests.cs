using FileWatcherService.Models;
using FileWatcherService.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;

namespace FileWatcherService.Tests.Services
{
    [TestFixture]
    public class AggregatedSenderTests
    {
        [Test]
        public void WhenAggregateSenderSucess()
        {      
            var senderMock1 = new Mock<ISender>();
            senderMock1.Setup(x => x.SendMessages(It.IsAny<IEnumerable<Message>>(), CancellationToken.None)).ReturnsAsync(true);            

            var senderMock2 = new Mock<ISender>();
            senderMock2.Setup(x => x.SendMessages(It.IsAny<IEnumerable<Message>>(), CancellationToken.None)).ReturnsAsync(true);

            var senders = new[] { senderMock1.Object, senderMock2.Object };

            var sender = new AggregatedSender(senders);

            var messages = new[]
            {
                new Message { Title = "Message1" },
                new Message { Title = "Message2" }
            };

            var actual = sender.SendMessages(messages, CancellationToken.None).Result;
            Assert.AreEqual(true, actual);

            senderMock1.Verify(x => x.SendMessages(It.IsAny<IEnumerable<Message>>(), CancellationToken.None), Times.Exactly(1));
            senderMock2.Verify(x => x.SendMessages(It.IsAny<IEnumerable<Message>>(), CancellationToken.None), Times.Exactly(1));
        }
    }
}
