using System.Threading.Tasks;

public interface IOrderService
{
    Task<string> CreateAsync(CreateOrderDto dto);
    Task EditAsync(EditOrderDto dto);
    Task ApproveAsync(ApproveOrderDto dto);
}
