using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderTaskHandler.Ddos;
using OrderTaskHandler.Services;

namespace OrderTaskHandler.Controllers
{
    [ApiController]
    [Route("api/order")]
    [Authorize(Roles = "hr,mp")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

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
}