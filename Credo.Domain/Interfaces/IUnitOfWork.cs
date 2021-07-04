using System;
using System.Collections.Generic;
using System.Text;
using Credo.Domain.Interfaces.IoC;
using Credo.Domain.Interfaces.Repositories;

namespace Credo.Domain.Interfaces
{
    public interface IUnitOfWork : IScoped
    {
        public IUserRepository UserRepository { get;}
    }
}
