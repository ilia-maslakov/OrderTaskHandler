using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SampleCamundaWorker.Ddos;
using SampleCamundaWorker.Infrastructure.Camunda.Models;

namespace SampleCamundaWorker.Services
{

    /// <summary>
    /// Клиент для взаимодействия с Camunda BPM.
    /// </summary>
    public class CamundaClient : ICamundaClient
    {
        private readonly HttpClient _http;

        public CamundaClient(HttpClient http)
        {
            _http = http;
        }

        /// <inheritdoc />
        public async Task StartProcessInstanceAsync(string processKey, string businessKey, CamundaVariables variables)
        {
            var payload = new
            {
                businessKey,
                variables
            };

            await _http.PostAsJsonAsync($"/engine-rest/process-definition/key/{processKey}/start", payload);
        }

        /// <inheritdoc />
        public async Task CompleteUserTaskAsync(string businessKey, string formKey, CamundaVariables variables)
        {
            var tasks = await _http.GetFromJsonAsync<List<CamundaUserTask>>(
                $"/engine-rest/task?processInstanceBusinessKey={businessKey}");

            var task = (tasks?.FirstOrDefault(t => t.FormKey == formKey))
                ?? throw new InvalidOperationException($"UserTask с formKey = '{formKey}' не найдена для процесса с ключом '{businessKey}'.");

            var payload = new { variables };
            await _http.PostAsJsonAsync($"/engine-rest/task/{task.Id}/complete", payload);
        }

        /// <inheritdoc />
        public async Task<ProcessInstanceInfo> GetProcessInfoAsync(string processId)
        {
            var result = await _http.GetFromJsonAsync<ProcessInstanceInfo>($"/engine-rest/process-instance/{processId}")
                ?? throw new InvalidOperationException($"Процесс с ID '{processId}' не найден.");
            return result;
        }

    }
}