using System.Threading.Tasks;

/// <summary>
/// Интерфейс для сервиса управления заказами.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Создает новый заказ.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<string> CreateOrderAsync(CreateOrderDto dto);

    /// <summary>
    /// Редактирует существующий заказ.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task EditOrderAsync(EditOrderDto dto);

    /// <summary>
    /// Одобряет заказ, который был создан ранее и ожидает одобрения.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task ApproveOrderAsync(ApproveOrderDto dto);
}
