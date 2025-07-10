using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OrderTaskHandler.Ddos;
using OrderTaskHandler.Infrastructure.Camunda.Models;

namespace OrderTaskHandler.Services
{
    /// <summary>
    /// Клиент для взаимодействия с Camunda BPM.
    /// </summary>
    public class CamundaClient(HttpClient http) : ICamundaClient
    {
        private readonly HttpClient _http = http;

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

        /// <inheritdoc />
        public async Task CompleteExternalTaskAsync(string taskId, CamundaVariables variables)
        {
            var payload = new
            {
                variables = variables.ToDictionary(
                    kvp => kvp.Key,
                    kvp => new
                    {
                        value = kvp.Value.Value,
                        type = kvp.Value.Type
                    })
            };

            var response = await _http.PostAsJsonAsync($"/engine-rest/external-task/{taskId}/complete", payload);

            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc />
        public async Task CorrelateMessageAsync(string messageName, string businessKey, CamundaVariables variables)
        {
            var payload = new
            {
                messageName,
                businessKey,
                variables = variables.ToDictionary(
                    kvp => kvp.Key,
                    kvp => new
                    {
                        value = kvp.Value.Value,
                        type = kvp.Value.Type
                    })
            };

            var response = await _http.PostAsJsonAsync("/engine-rest/message", payload);
            response.EnsureSuccessStatusCode();
        }
    }
}
