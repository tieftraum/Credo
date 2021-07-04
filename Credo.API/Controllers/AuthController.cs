using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Credo.Domain.Dtos.Commands;
using Credo.Domain.Interfaces;
using Credo.Domain.Interfaces.Services;

namespace Credo.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto model)
        {
            var registerResult = await _authService.RegisterAsync(model);

            if (!registerResult.Success)
            {
                return BadRequest(registerResult.ErrorMessages);
            }

            return Ok(registerResult.Token);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(string personalNumber, string password)
        {
            var registerResult = await _authService.LoginAsync(personalNumber, password);

            if (!registerResult.Success)
            {
                return BadRequest(registerResult.ErrorMessages);
            }

            return Ok(registerResult.Token);
        }
    }
}
