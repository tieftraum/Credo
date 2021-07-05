using Credo.Domain.Dtos.Commands;
using Credo.Domain.Enums;
using Credo.Domain.Models;
using Credo.Domain.Request;

namespace Credo.Domain.Factories
{
    public static class LoanFactory
    {
        public static LoanCreateDto InitialLoanModel(LoanDemandRequest model, int userId)
        {
            //Can use AutoMapper actually but for simplicity I think its fine
            return new LoanCreateDto
            {
                Amount = model.Amount,
                CurrencyId = model.Currency,
                StartPeriod = model.StartPeriod,
                EndPeriod = model.EndPeriod,
                IsDeleted = false,
                LoanType = model.LoanType,
                UserId = userId,
                Status = LoanStatus.Forwarded
            };
        }

        public static LoanUpdateDto UpdateLoanModel(LoanUpdateRequest model)
        {
            return new LoanUpdateDto
            {
                Amount = model.Amount,
                CurrencyId = model.CurrencyId,
                StartPeriod = model.StartPeriod,
                EndPeriod = model.EndPeriod,
                IsDeleted = false,
                LoanType = model.LoanType,
            };
        }
    }
}