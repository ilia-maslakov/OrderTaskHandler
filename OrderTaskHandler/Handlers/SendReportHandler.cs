using System.Threading;
using System.Threading.Tasks;
using Camunda.Worker;

namespace OrderTaskHandler.Handlers
{
    [HandlerTopics("sendReport", LockDuration = 10000)]
    [HandlerVariables]
    public class SendReportHandler : IExternalTaskHandler
    {
        public Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            
            return Task.FromResult<IExecutionResult>(new CompleteResult
            {
                Variables = []
            });
        }
    }
}
