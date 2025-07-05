using System;
using System.Threading.Tasks;

public class OrderService : IOrderService
{
    private readonly ICamundaClient _camunda;

    public OrderService(ICamundaClient camunda)
    {
        _camunda = camunda;
    }

    public async Task<string> CreateAsync(CreateOrderDto dto)
    {
        var businessKey = Guid.NewGuid().ToString();

        var variables = new
        {
            name = new { value = dto.Name, type = "String" }
        };

        await _camunda.StartProcessInstanceAsync("say-hello", businessKey, variables);

        return businessKey;
    }

    public async Task EditAsync(EditOrderDto dto)
    {
        var variables = new { name = new { value = dto.Name, type = "String" } };
        await _camunda.CompleteUserTaskAsync(dto.TaskId, variables);
    }

    public async Task ApproveAsync(ApproveOrderDto dto)
    {
        var variables = new { approve = new { value = dto.Approve ? "yes" : "no", type = "String" } };
        await _camunda.CompleteUserTaskAsync(dto.TaskId, variables);
    }
}
