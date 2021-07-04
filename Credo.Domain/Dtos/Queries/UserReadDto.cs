using System;
using Credo.Domain.Models;

namespace Credo.Domain.Dtos.Queries
{
    public class UserReadDto : User
    {
        public int Id { get; set; }
    }
}