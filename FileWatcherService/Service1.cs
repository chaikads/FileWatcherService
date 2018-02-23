using System.ServiceProcess;
using System.Threading;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using FileWatcherService.Services;
using Slack.Webhooks;

namespace FileWatcherService
{
    public partial class Service1 : ServiceBase
    {
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        private readonly INotifier notifier;
        

        public Service1()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = false;
            this.AutoLog = true; 

            var configuration = new Configuration();

            var credentials = new VssBasicCredential(string.Empty, configuration.TfsToken);
            var connection = new VssConnection(configuration.TfsUri, credentials);
            var tfsClient = connection.GetClient<WorkItemTrackingHttpClient>();
            
            var receiver = new TfsNotUpdatedReceiver(tfsClient, configuration);
            var receiver2 = new TfsReceiver1(tfsClient, configuration);

            var slackClient = new SlackClient(configuration.SlackUri.ToString());
            var sender = new SlackSender(slackClient, configuration);

            var logger = new EventLogLogger(configuration);

            var aggregatedReceiver = new AggregatedReceiver(receiver, receiver2);
            var aggregatedSender = new AggregatedSender(sender);
      
            this.notifier = new Notifier(aggregatedReceiver, aggregatedSender, logger, configuration);
        }

        protected override void OnStart(string[] args)
        {     
           this.notifier.Run(cts.Token);
        }
        
        protected override void OnStop()
        {
            cts.Cancel();
            this.notifier.Dispose();
        }        
    }
}
