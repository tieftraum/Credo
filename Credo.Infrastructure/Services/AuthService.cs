using System.Collections.Generic;
using System.Threading.Tasks;
using Credo.Domain.Dtos.Commands;
using Credo.Domain.Interfaces;
using Credo.Domain.Interfaces.Repositories;
using Credo.Domain.Interfaces.Services;
using Credo.Domain.Response;

namespace Credo.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public async Task<AuthenticationResult> RegisterAsync(UserCreateDto model)
        {
            var existingUser = await _unitOfWork.UserRepository.GetUserByPersonalNumber(model.PersonalNumber);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new List<string>
                    {
                        "User with this personal number already exists"
                    }
                };
            }

            var newUserId = await _unitOfWork.UserRepository.AddUser(model);

            if (newUserId <= 0)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new List<string>
                    {
                        "Problem registering user"
                    }
                };
            }

            var token = await _tokenService.CreateToken(model, newUserId);

            return new AuthenticationResult
            {
                Success = true,
                Token = token
            };
        }
        public async Task<AuthenticationResult> LoginAsync(string personalNumber, string password)
        {
            var userToLogin = await _unitOfWork.UserRepository.GetUserByPersonalNumber(personalNumber);
            
            if (userToLogin == null)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new List<string>
                    {
                        "User with this personal number doesn't exist"
                    }
                };
            }

            if (password != userToLogin.Password)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new List<string>
                    {
                        "Personal number or password invalid"
                    },
                };
            }

            var token = await _tokenService.CreateToken(userToLogin, userToLogin.Id);

            return new AuthenticationResult
            {
                Success = true,
                Token = token
            };
        }
    }
}