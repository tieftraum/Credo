using System;
using Credo.Domain.Enums;

namespace Credo.Domain.Models
{
    public class Loan : BaseObject
    {
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public Currency CurrencyId { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public LoanStatus Status { get; set; }
    }
}