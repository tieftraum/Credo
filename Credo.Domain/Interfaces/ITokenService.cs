using System.Threading.Tasks;
using Credo.Domain.Dtos.Commands;
using Credo.Domain.Interfaces.IoC;
using Credo.Domain.Models;

namespace Credo.Domain.Interfaces
{
    public interface ITokenService : IScoped
    {
        Task<string> CreateToken(User user,int userId);
    }
}