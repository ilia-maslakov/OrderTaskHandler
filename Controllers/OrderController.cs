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
        var businessKey = await _orderService.CreateOrderAsync(dto);
        return Ok(businessKey);
    }

    [HttpPost("edit")]
    public async Task<IActionResult> Edit([FromBody] EditOrderDto dto)
    {
        await _orderService.EditOrderAsync(dto);
        return Ok();
    }

    [HttpPost("approve")]
    public async Task<IActionResult> Approve([FromBody] ApproveOrderDto dto)
    {
        await _orderService.ApproveOrderAsync(dto);
        return Ok();
    }
}
