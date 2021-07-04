using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Credo.Domain.Dtos.Commands;
using Credo.Domain.Dtos.Queries;
using Credo.Domain.Interfaces.IoC;
using Credo.Domain.Response;

namespace Credo.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IScoped
    {
        Task<UserReadDto> GetUserById(int id);
        Task<UserReadDto> GetUserByPersonalNumber(string personalNumber);
        Task<IEnumerable<UserReadDto>> GetUsers(int amount);
        Task<int> AddUser(UserCreateDto user);
    }
}
