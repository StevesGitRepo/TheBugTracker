﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheBugTracker.Models.ViewModels
{
    public class ManageUserRolesViewModel
    {
        //properties
        public BTUser BTUser{ get; set; }

        public MultiSelectList Roles { get; set; }

        public List<string> SelectedRoles { get; set; }

    }
}
