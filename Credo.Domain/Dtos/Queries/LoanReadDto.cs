using Credo.Domain.Enums;
using Credo.Domain.Models;

namespace Credo.Domain.Dtos.Queries
{
    public class LoanReadDto : Loan
    {
        public int Id { get; set; }
    }
}