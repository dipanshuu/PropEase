using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PropEase.Models;
using PropEase.Repository.UserRole;

namespace PropEase.Filters
{
    public class AuthorizeRoleFilter:TypeFilterAttribute
    {
        public AuthorizeRoleFilter(params string[] roles):base(typeof(AuthorizeRolesFilter))
        {
            Arguments=new Object[] {roles};
        }
    }
    public class AuthorizeRolesFilter:IAuthorizationFilter { 
    private readonly string[] roles;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthorizeRolesFilter(string[] roles, IUserRoleRepository userRoleRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.roles = roles;
            _userRoleRepository = userRoleRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Fetch user ID from claims
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                context.Result = new ForbidResult(); // User is not authenticated
                return;
            }

            // Validate user ID
            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                context.Result = new ForbidResult(); // Invalid user ID format
                return;
            }

            // Perform async operation synchronously (fetch roles)
            var userRoles = Task.Run(() => _userRoleRepository.GetUserRolesAsync(userId)).Result;

            // Check if user has required role(s)
            if (!userRoles.Any(role => userRoles.Contains(role)))
            {
                context.Result = new ForbidResult(); // User does not have required roles
                return;
            }
        }

    }
}
