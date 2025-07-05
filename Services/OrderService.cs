using System;
using System.Threading.Tasks;

/// <summary>
/// Сервис для управления заказами.
/// </summary>
public class OrderService : IOrderService
{
    private readonly ICamundaClient _camunda;
    const string BPMN_ORDER_PROCESS_NAME = "process-t";

    /// <summary>
    /// Инициализирует новый экземпляр сервиса заказов.
    /// </summary>
    /// <param name="camunda"></param>
    public OrderService(ICamundaClient camunda)
    {
        _camunda = camunda;
    }

    /// <summary>
    /// Создает новый заказ.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Возвращает ID bpmn процесса.</returns>
    public async Task<string> CreateOrderAsync(CreateOrderDto dto)
    {
        var businessKey = Guid.NewGuid().ToString();

        var variables = new
        {
            name = new { value = dto.Name, type = "String" }
        };

        await _camunda.StartProcessInstanceAsync(BPMN_ORDER_PROCESS_NAME, businessKey, variables);

        return businessKey;
    }

    /// <summary>
    /// Редактирует существующий заказ.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task EditOrderAsync(EditOrderDto dto)
    {
        var variables = new { name = new { value = dto.Name, type = "String" } };
        await _camunda.CompleteUserTaskAsync(dto.TaskId, variables);
    }

    /// <summary>
    /// Одобряет заказ, который был создан ранее и ожидает одобрения.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task ApproveOrderAsync(ApproveOrderDto dto)
    {
        var variables = new { approve = new { value = dto.Approve ? "yes" : "no", type = "String" } };
        await _camunda.CompleteUserTaskAsync(dto.TaskId, variables);
    }
}
