using Microsoft.AspNetCore.Mvc;
using OrderService.Service.Interfaces;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> Login(string login, string password) =>
            Ok(await this.authService.GenerateTokenAsync(login, password));
    }
}
