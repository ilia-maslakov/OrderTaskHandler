using System.Text.Json.Serialization;

namespace OrderTaskHandler.Infrastructure.Camunda.Models
{
    public class CamundaVariable(object value, string type = "String")
    {
        [JsonPropertyName("value")]
        public object Value { get; set; } = value;

        [JsonPropertyName("type")]
        public string Type { get; set; } = type;

        public CamundaVariable(object value) : this(value, "String")
        {
        }
    }
}
