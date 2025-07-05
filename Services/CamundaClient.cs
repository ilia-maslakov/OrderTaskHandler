using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SampleCamundaWorker.Ddos;

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
    public async Task StartProcessInstanceAsync(string processKey, string businessKey, object variables)
    {
        var payload = new { variables };
        await _http.PostAsJsonAsync($"/engine-rest/process-definition/key/{processKey}/start", payload);
    }

    /// <inheritdoc />
    public async Task CompleteUserTaskAsync(string taskId, object variables)
    {
        var payload = new { variables };
        await _http.PostAsJsonAsync($"/engine-rest/task/{taskId}/complete", payload);
    }

    /// <inheritdoc />
    public async Task<ProcessInstanceInfo> GetProcessInfoAsync(string processId)
    {
        var result = await _http.GetFromJsonAsync<ProcessInstanceInfo>($"/engine-rest/process-instance/{processId}")
            ?? throw new InvalidOperationException($"Процесс с ID '{processId}' не найден.");
        return result;
    }

}
