using System;
using Credo.Domain.Enums;

namespace Credo.Domain.Request
{
    public class LoanDemandRequest 
    {
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
    }
}