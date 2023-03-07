using System.Security.Claims;

namespace AlumniNetworkAPI.Helpers
{
    public static class UserHelper
    {
        public static string GetId(this ClaimsPrincipal principal)
        {
            var p = principal;
            if (p != null)
            {
                return p.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return null;
        }

        public static string GetUsername(this ClaimsPrincipal principal)
        {
            var p = principal;
            if (p != null)
            {
                return p.FindFirstValue("preferred_username");
            }
            return null;
        }
    }
}
