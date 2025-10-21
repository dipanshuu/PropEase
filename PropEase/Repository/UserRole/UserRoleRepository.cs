using Microsoft.EntityFrameworkCore;
using PropEase.Context;

namespace PropEase.Repository.UserRole
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDBContext _context;
        public UserRoleRepository(AppDBContext context)
        {
            _context = context;
        }

       public async Task<List<string>>  GetUserRolesAsync(int userId)
        {
            return await _context.UserRoles.Where(ur=>ur.UserId==userId).Select(ur=>ur.Role.RoleName).ToListAsync();
        }
    }
}
