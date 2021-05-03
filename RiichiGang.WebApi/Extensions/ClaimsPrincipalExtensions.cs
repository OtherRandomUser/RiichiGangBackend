using System.Linq;
using System.Security.Claims;

namespace RiichiGang.WebApi.Extensios
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Username(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        public static string Email(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    }
}
