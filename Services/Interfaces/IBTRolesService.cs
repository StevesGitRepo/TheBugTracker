using TheBugTracker.Models;

namespace TheBugTracker.Services.Interfaces
{
    //There is no code but the definition of the methods or properties in an interface
    //Classes contain implementation code
    //everything within an Interface is public, Interfaces contain methods (functions) for classes
    public interface IBTRolesService
    {
        public Task<bool> IsUserInRoleAsync(BTUser user, string rolename);

        public Task<IEnumerable<string>> GetUserRolesAsync(BTUser user);

        public Task<bool> AddUserToRoleAsync(BTUser user, string rolename);

        public Task<bool> RemoveUserFromRoleAsync(BTUser user, string rolename);

        public Task<bool> RemoveUserFromRolesAsync(BTUser user, IEnumerable<string> roles);

        public Task<List<BTUser>> GetUsersInRoleAsync(string rolename, int companyId);

        public Task<List<BTUser>> GetUsersNotInRoleAsync(string rolename, int companyId);

        public Task<string> GetRoleNameByIdAsync(string roleId);
    
    }
}
