using System;
using System.Security.Claims;

namespace Blog.DataAccess.Extensions
{
	public static class CurrentUserExtensions
	{
		public static Guid GetUserId(this ClaimsPrincipal principal)
		{
			return Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
		}
        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);
        }
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Email);
        }
    }
}

