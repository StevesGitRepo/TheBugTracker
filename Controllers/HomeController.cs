﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TheBugTracker.Extensions;
using TheBugTracker.Models;
using TheBugTracker.Models.ViewModels;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBTCompanyInfoService _companyInfoService;
        private readonly IBTProjectService _projectService;



        public HomeController(ILogger<HomeController> logger,
                              IBTCompanyInfoService companyInfoService,
                              IBTProjectService projectService)

        {
            _logger = logger;
            _companyInfoService = companyInfoService;
            _projectService = projectService;
        }


        public IActionResult Index()
        {
            return View();
        }

        //GET Action
        public async Task<IActionResult> Dashboard()
        {
            //instantiate viewmodel
            DashboardViewModel model = new();
            int companyId = User.Identity.GetCompanyId().Value;

            model.Company = await _companyInfoService.GetCompanyInfoByIdAsync(companyId);

            model.Projects = (await _companyInfoService.GetAllProjectsAsync(companyId)).Where(p => p.Archived == false).ToList();

            model.Tickets = model.Projects.SelectMany(p => p.Tickets).Where(t=>t.Archived == false).ToList();

            model.Members = model.Company.Members.ToList();


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}