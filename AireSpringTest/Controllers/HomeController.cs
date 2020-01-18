using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AireSpringTest.Models;
using AireSpringTest.Services;

namespace AireSpringTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;
        public HomeController(IEmployeeService employeeService, ILogger<HomeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Error = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Index(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employeeExists = await _employeeService.EmployeeExistAsync(model.EmployeeCode);
                if (employeeExists)
                {
                    ViewBag.Error = true;
                    ViewBag.Message = $"Employee with code {model.EmployeeCode} already exists.";
                    return View(model);
                }
                else
                {
                    await _employeeService.CreateEmployeeAsync(model.EmployeeCode, model.FirstName, model.LastName,
                        model.PhoneNumber, model.ZipCode, model.HireDate);
                    ViewBag.Error = false;
                    ViewBag.Message = $"Employee with code {model.EmployeeCode} successfully saved";
                    return View();
                }
            }
            ViewBag.Error = true;
            ViewBag.Message = $"Invalid data submitted";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Employees(int page,int limit, string filter = default)
        {
            limit = limit <= 0 ? 2 : limit;
            page = Math.Max(1, page);
            var employees = await _employeeService.GetEmployeesAsync(page , limit, filter, "HireDate");
            var items = employees.Rows.Select(e => new EmployeeViewModel
            {
                EmployeeCode = e.EmployeeCode, LastName = e.LastName, FirstName = e.FirstName,
                PhoneNumber = e.PhoneNumber, ZipCode = e.ZipCode, HireDate = e.HireDate
            });
            var model = new SearchModel<EmployeeViewModel>{Filter = filter, Limit = limit, Page = page, TotalItems = employees.TotalCount, Items = items};
            return View(model);
        }

    }
}
