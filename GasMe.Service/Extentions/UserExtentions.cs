using System.Linq;
using Microsoft.AspNetCore.Http;

namespace GasMe.Service.Extentions
{
    public static class UserExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if(httpContext.User == null) return string.Empty;
            return httpContext.User.Claims.Single(x => x.Type == "identityUserId").Value;
        }
    }
}