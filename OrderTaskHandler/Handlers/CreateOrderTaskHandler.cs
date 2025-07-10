using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Camunda.Worker;
using Camunda.Worker.Variables;
using OrderTaskHandler.Infrastructure.Camunda.Models;

namespace OrderTaskHandler.Handlers
{
    [HandlerTopics("orderTask", LockDuration = 10000)]
    public class CreateOrderTaskHandler : IExternalTaskHandler
    {
        public Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            var variables = new CamundaVariables
            {
                ["status"] = new CamundaVariable("Approved"),
                ["timestamp"] = new CamundaVariable(DateTime.UtcNow.ToString("o"))
            };

            var resultVariables = new Dictionary<string, VariableBase>
            {
                ["status"] = new StringVariable((string)variables["status"].Value),
                ["timestamp"] = new StringVariable((string)variables["timestamp"].Value)
            };

            return Task.FromResult<IExecutionResult>(new CompleteResult
            {
                Variables = resultVariables
            });
        }
    }
}
