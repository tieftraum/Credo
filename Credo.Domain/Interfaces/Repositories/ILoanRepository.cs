using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Credo.Domain.Dtos.Commands;
using Credo.Domain.Dtos.Queries;
using Credo.Domain.Interfaces.IoC;

namespace Credo.Domain.Interfaces.Repositories
{
    public interface ILoanRepository : IScoped
    {
        Task<int> DemandLoan(LoanCreateDto model, int userId);
        Task<IEnumerable<LoanReadDto>> LoansByUserId(int userId);
        Task<LoanReadDto> GetLoanById(int id, int userId);
        Task<int> EditLoan(int loanId, LoanUpdateDto model);
    }
}
