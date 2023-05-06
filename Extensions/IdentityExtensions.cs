using System.Security.Claims;
using System.Security.Principal;

namespace TheBugTracker.Extensions
{
    public static class IdentityExtensions
    {
        public static int? GetCompanyId(this IIdentity identity)
        {
            //FindFirst gives immediate access to the first claim that matches the specified type
            Claim claim = ((ClaimsIdentity)identity).FindFirst("CompanyId");
            //Ternary operator (if/else)
            return (claim != null) ? int.Parse(claim.Value) : null;
        }
    }
}
