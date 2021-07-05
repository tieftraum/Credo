using System;
using System.Collections.Generic;
using System.Text;
using Credo.Domain.Enums;

namespace Credo.Domain.Request
{
    public class LoanUpdateRequest
    {
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public Currency CurrencyId { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
    }
}
