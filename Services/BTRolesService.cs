using Microsoft.AspNetCore.Identity;
using TheBugTracker.Data;
using TheBugTracker.Models;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Services
{
    //Class contains implementation code
    public class BTRolesService : IBTRolesService
    {
        //Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BTUser> _userManager;
        public BTRolesService(ApplicationDbContext context,
                                RoleManager<IdentityRole> roleManager,
                                UserManager<BTUser> userManager
                                ) 
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        //add async to crate asynchronous methods
        public async Task<bool> AddUserToRoleAsync(BTUser user, string rolename)
        {
            bool result = (await _userManager.AddToRoleAsync(user, rolename)).Succeeded;
            return result;
        }

        public Task<string> GetRoleNameByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetUserRoleAsync(BTUser user)
        {
            throw new NotImplementedException();
        }

        public Task<List<BTUser>> GetUsersInRoleAsync(string rolename, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BTUser>> GetUsersNotInRolseAsync(string rolename, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserInRoleAsync(BTUser user, string rolename)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromRoleAsync(BTUser user, string rolename)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromRolesAsync(BTUser user, IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}
