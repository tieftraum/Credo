using Credo.Domain.Dtos.Commands;
using Credo.Domain.Dtos.Queries;
using Credo.Domain.Interfaces.Repositories;
using Credo.Domain.Interfaces.Services;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Credo.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperService _dapperService;
        public UserRepository(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }
        public async Task<UserReadDto> GetUserById(int id)
        {
            const string query = @"SELECT * FROM [CredoAPI].[dbo].[Users] WHERE Id = @id";
            await using var conn = new SqlConnection(_dapperService.GetCredoApiConnectionString());
            return await conn.QueryFirstOrDefaultAsync<UserReadDto>(query, new {id});
        }
        public async Task<IEnumerable<UserReadDto>> GetUsers(int amount)
        {
            const string query = @"SELECT TOP (@amount) * FROM [CredoAPI].[dbo].[Users]";
            await using var conn = new SqlConnection(_dapperService.GetCredoApiConnectionString());
            return await conn.QueryAsync<UserReadDto>(query, new {amount});

        }
        public async Task<int> AddUser(UserCreateDto user)
        {
            const string command = "INSERT INTO [CredoAPI].[dbo].[Users] (Firstname, Lastname, PersonalNumber, Password, DoB, Country, City, Address, CreatedAt) Values (@Firstname, @Lastname, @PersonalNumber, @Password, @DoB, @Country, @City, @Address, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int)";
            await using var conn = new SqlConnection(_dapperService.GetCredoApiConnectionString());
            return await conn.ExecuteScalarAsync<int>(command, user);
        }
    }
}
