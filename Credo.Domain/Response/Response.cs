using System;
using System.Collections.Generic;
using System.Text;

namespace Credo.Domain.Response
{
    public class Response<T>
    {
        public Response(T data, bool success)
        {
            Data = data;
            Success = success;
            Sent = DateTime.UtcNow;;
        }

        public T Data { get; set; }
        public bool Success { get; set; }
        public DateTime Sent { get; set; }
    }
}
