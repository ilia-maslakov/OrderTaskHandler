using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderTaskHandler.Ddos;
using OrderTaskHandler.Services;

namespace OrderTaskHandler.Controllers
{
    [ApiController]

    [Route("api/authorize")]
    [AllowAnonymous]
    public class AuthorizeController(IAuthorizeService authService) : ControllerBase
    {
        private readonly IAuthorizeService _authService = authService;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (!_authService.Validate(dto.Username, dto.Password))
                return Unauthorized("Неверные учетные данные");

            var role = _authService.GetRole(dto.Username);
            if (role == null)
                return Unauthorized("Пользователь не найден или не имеет роли");

            var token = _authService.GenerateToken(dto.Username, role);
            return Ok(new { token });
        }
    }
}