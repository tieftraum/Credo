using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Credo.Domain.Dtos.Commands;
using Credo.Domain.Interfaces;
using Credo.Domain.Interfaces.Services;
using Credo.Domain.Options;
using Credo.Domain.Request;
using Credo.Infrastructure.Helpers;
using Microsoft.Extensions.Options;

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

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] UserCreateDto model)
        {
            var registerResult = await _authService.RegisterAsync(model);

            if (!registerResult.Success)
            {
                return BadRequest(registerResult.ErrorMessages);
            }

            return Ok(registerResult.Token);
        }


        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var registerResult = await _authService.LoginAsync(model.PersonalNumber, model.Password);

            if (!registerResult.Success)
            {
                return BadRequest(registerResult.ErrorMessages);
            }

            return Ok(registerResult.Token);
        }
    }
}
