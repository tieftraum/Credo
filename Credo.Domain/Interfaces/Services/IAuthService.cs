using System.Threading.Tasks;
using Credo.Domain.Dtos.Commands;
using Credo.Domain.Interfaces.IoC;
using Credo.Domain.Response;

namespace Credo.Domain.Interfaces.Services
{
    public interface IAuthService : IScoped
    {
        Task<AuthenticationResult> RegisterAsync(UserCreateDto model);

        //პირადი ნომრით დალოგინების გზით წავედი and I don't know why :)) ალბათ უფრო იმიტომ რო მარტივია და რახან საჩვენებელი კოდია მარტივად ვუყურებ :)
        Task<AuthenticationResult> LoginAsync(string personalNumber, string password);
    }
}