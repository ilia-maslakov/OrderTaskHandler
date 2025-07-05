using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        await _orderService.CreateAsync(dto);
        return Ok();
    }

    [HttpPost("edit")]
    public async Task<IActionResult> Edit([FromBody] EditOrderDto dto)
    {
        await _orderService.EditAsync(dto);
        return Ok();
    }

    [HttpPost("approve")]
    public async Task<IActionResult> Approve([FromBody] ApproveOrderDto dto)
    {
        await _orderService.ApproveAsync(dto);
        return Ok();
    }
}
