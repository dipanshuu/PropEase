namespace PropEase.Repository.UserRole
{
    public interface IUserRoleRepository
    {
        Task<List<string>> GetUserRolesAsync(int userId);
            }
}