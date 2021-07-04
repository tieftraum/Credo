using System.Data;
using Credo.Domain.Interfaces.IoC;

namespace Credo.Domain.Interfaces.Services
{
    public interface IDapperService : IScoped
    {
        string GetLogsConnectionString();
        string GetCredoApiConnectionString();
        IDbConnection InitializeConnection(string connectionString);
    }
}
