using System.Text.Json.Serialization;

namespace SampleCamundaWorker.Infrastructure.Camunda.Models
{
    public class CamundaVariable
    {
        [JsonPropertyName("value")]
        public object Value { get; set; } = default!;

        [JsonPropertyName("type")]
        public string Type { get; set; } = default!;
    }
}
