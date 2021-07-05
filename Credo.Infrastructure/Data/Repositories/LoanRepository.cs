using Credo.Domain.Dtos.Commands;
using Credo.Domain.Dtos.Queries;
using Credo.Domain.Interfaces.Repositories;
using Credo.Domain.Interfaces.Services;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Credo.Infrastructure.Data.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly IDapperService _dapperService;

        public LoanRepository(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }
        public async Task<int> DemandLoan(LoanCreateDto model, int userId)
        {
            const string command = "INSERT INTO [CredoAPI].[Loans].[Loans] " +
                                   "(LoanType, Amount, UserId, CurrencyId, StartPeriod, EndPeriod, Status, IsDeleted, CreatedAt) " +
                                   "Values " +
                                   "(@LoanType, @Amount, @UserId, @CurrencyId, @StartPeriod, @EndPeriod, @Status, @IsDeleted, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.AddDynamicParams(new { model.LoanType, model.Amount, userId, model.CurrencyId, model.StartPeriod, model.EndPeriod, model.Status, model.IsDeleted, model.CreatedAt });

            await using var conn = new SqlConnection(_dapperService.GetCredoApiConnectionString());
            return await conn.ExecuteScalarAsync<int>(command, parameters);
        }
        public async Task<IEnumerable<LoanReadDto>> LoansByUserId(int userId)
        {
            //this query can be filtered
            const string query = @"SELECT * FROM [CredoAPI].[Loans].[Loans] where UserId = @userId AND Status <> 2 AND Status <> 3";
            await using var conn = new SqlConnection(_dapperService.GetCredoApiConnectionString());
            return await conn.QueryAsync<LoanReadDto>(query, new { userId });
        }
        public async Task<LoanReadDto> GetLoanById(int id, int userId)
        {
            const string query = @"SELECT * FROM [CredoAPI].[Loans].[Loans] WHERE Id = @id AND UserId = @userId AND Status <> 2 AND Status <> 3";
            await using var conn = new SqlConnection(_dapperService.GetCredoApiConnectionString());
            return await conn.QueryFirstOrDefaultAsync<LoanReadDto>(query, new { id, userId });
        }

        public async Task<int> EditLoan(int loanId, LoanUpdateDto model)
        {
            const string command = @"UPDATE [CredoAPI].[Loans].[Loans] 
                                    SET LoanType = @LoanType, Amount = @Amount, CurrencyId = @CurrencyId, StartPeriod = @StartPeriod, EndPeriod = @EndPeriod WHERE Id = @LoanId";
            await using var conn = new SqlConnection(_dapperService.GetCredoApiConnectionString());
            var parameters = new DynamicParameters();
            parameters.Add("@LoanType", model.LoanType);
            parameters.Add("@Amount", model.Amount);
            parameters.Add("@CurrencyId", model.CurrencyId);
            parameters.Add("@StartPeriod", model.StartPeriod);
            parameters.Add("@EndPeriod", model.EndPeriod);
            parameters.Add("@LoanId", loanId);

            return await conn.ExecuteAsync(command, parameters);
        }
    }
}