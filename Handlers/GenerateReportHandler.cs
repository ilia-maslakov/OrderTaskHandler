using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Camunda.Worker;
using Camunda.Worker.Variables;
using SampleCamundaWorker.Services;

namespace SampleCamundaWorker.Handlers
{
    /// <summary>
    /// Инициализирует новый экземпляр обработчика задач генерации отчетов.
    /// </summary>
    /// <param name="reportService"></param>
    /// <exception cref="ArgumentNullException"></exception>
    [HandlerTopics("generateReport", LockDuration = 10000)]
    public class GenerateReportHandler(IReportService reportService) : IExternalTaskHandler
    {
        private readonly IReportService _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));

        public Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            var businessKey = externalTask.BusinessKey
                              ?? throw new InvalidOperationException("BusinessKey is required");

            // Запускаем фоновую генерацию с коллбэком, который вызовет сообщение в Camunda
            _ = _reportService.GenerateReportAsync(businessKey, cancellationToken);

            // Завершаем задачу сразу
            return Task.FromResult<IExecutionResult>(new CompleteResult());
        }
    }
}
