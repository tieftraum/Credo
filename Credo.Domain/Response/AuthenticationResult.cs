using System.Collections.Generic;

namespace Credo.Domain.Response
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}