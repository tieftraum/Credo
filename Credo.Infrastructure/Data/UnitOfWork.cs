using System;
using System.Collections.Generic;
using System.Text;
using Credo.Domain.Interfaces;
using Credo.Domain.Interfaces.Repositories;
using Credo.Domain.Interfaces.Services;
using Credo.Infrastructure.Data.Repositories;

namespace Credo.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDapperService _dapperService;

        public UnitOfWork(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        public IUserRepository UserRepository => new UserRepository(_dapperService);
        public ILoanRepository LoanRepository => new LoanRepository(_dapperService);
    }
}
