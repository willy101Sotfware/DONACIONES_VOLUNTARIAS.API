using Microsoft.AspNetCore.Mvc;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Services.Autentication;

namespace DONACIONES_VOLUNTARIAS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.ValidateUser(request.Username, request.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = await _authService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }
}
