using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Credo.API.Extensions
{
    public static class UserExtensions
    {
        public static int GetActiveUserId(this HttpContext context)
        {
            var userId = context.User == null ? string.Empty : context.User.Claims.SingleOrDefault(x => x.Type == "Id")?.Value;
            return ConvertedUserId(userId);
        }
        private static int ConvertedUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(userId))
            {
                return 0;
            }

            var success = int.TryParse(userId, out var id);
            return success ? id : 0;
        }
    }
}
