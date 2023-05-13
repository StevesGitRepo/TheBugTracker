using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheBugTracker.Extensions;
using TheBugTracker.Models;
using TheBugTracker.Models.ViewModels;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Controllers
{
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
    }
}
