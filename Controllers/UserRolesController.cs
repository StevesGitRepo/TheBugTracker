using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheBugTracker.Extensions;
using TheBugTracker.Models;
using TheBugTracker.Models.ViewModels;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Controllers
{
    [Authorize]
    public class UserRolesController : Controller
    {
        private readonly IBTRolesService _rolesService;
        private readonly IBTCompanyInfoService _companyInfoService;

        public UserRolesController(IBTCompanyInfoService companyInfoService, 
                                   IBTRolesService rolesService)
        {
            _companyInfoService = companyInfoService;
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles()
        {

            // add an instance of the ViewModel as a list
            List<ManageUserRolesViewModel> model = new();

            // get CompanyId
            int companyId = User.Identity.GetCompanyId().Value;

            // get all users in the company
            List<BTUser> users = await _companyInfoService.GetAllMembersAsync(companyId);
            // instantiate ViewModel


            // use _roleService


            // Create multi-selects
            foreach(BTUser user in users)
            {
                ManageUserRolesViewModel viewModel = new();
                viewModel.BTUser = user;
                IEnumerable<string> selected = await _rolesService.GetUserRolesAsync(user);
                viewModel.Roles = new MultiSelectList(await _rolesService.GetRolesAsync(), "Name", "Name", selected);

                model.Add(viewModel);
            }

            // return the model to the view
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(ManageUserRolesViewModel member)
        {
            // get the company id
            int companyId = User.Identity.GetCompanyId().Value;
            
            // Instantiate the BTUser
            BTUser btUser = (await _companyInfoService.GetAllMembersAsync(companyId)).FirstOrDefault(u => u.Id == member.BTUser.Id);

            // Get the roles for the user
            IEnumerable<string> roles = await _rolesService.GetUserRolesAsync(btUser);


            // Grab the selected roles  
            string userRole = member.SelectedRoles.FirstOrDefault();

            if (!string.IsNullOrEmpty(userRole))
            {
                // Remove the user from the roles
                if(await _rolesService.RemoveUserFromRolesAsync(btUser, roles))
                {
                    // Add the user to the role
                    await _rolesService.AddUserToRoleAsync(btUser, userRole);
                }
            }
            
            // return to the view
            return RedirectToAction(nameof(ManageUserRoles));
        }
    }
}
