using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class CamundaClient : ICamundaClient
{
    private readonly HttpClient _http;

    public CamundaClient(HttpClient http)
    {
        _http = http;
    }

    public async Task StartProcessInstanceAsync(string processKey, string businessKey, object variables)
    {
        var payload = new { variables };
        await _http.PostAsJsonAsync($"/engine-rest/process-definition/key/{processKey}/start", payload);
    }

    public async Task CompleteUserTaskAsync(string taskId, object variables)
    {
        var payload = new { variables };
        await _http.PostAsJsonAsync($"/engine-rest/task/{taskId}/complete", payload);
    }
}
