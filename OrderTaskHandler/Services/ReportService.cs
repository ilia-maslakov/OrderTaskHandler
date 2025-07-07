using System;
using System.Threading;
using System.Threading.Tasks;
using SampleCamundaWorker.Infrastructure.Camunda.Models;

namespace SampleCamundaWorker.Services
{
    public class ReportService(ICamundaClient camundaClient) : IReportService
    {
        private readonly ICamundaClient _camundaClient = camundaClient ?? throw new ArgumentNullException(nameof(camundaClient));

        public async Task GenerateReportAsync(string businessKey, CancellationToken cancellationToken)
        {
             await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);

            var variables = new CamundaVariables
            {
                ["reportId"] = new CamundaVariable(Guid.NewGuid(), "String")
            };

            await _camundaClient.CorrelateMessageAsync("ReportGenerated", businessKey, variables);
        }
    }
}
