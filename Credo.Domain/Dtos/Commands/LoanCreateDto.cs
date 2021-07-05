using Credo.Domain.Enums;
using Credo.Domain.Models;

namespace Credo.Domain.Dtos.Commands
{
    public class LoanCreateDto : Loan
    {
        public int UserId { get; set; }
    }
}