using ASPNETMVC.Data;
using ASPNETMVC.Models;
using ASPNETMVC.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ASPNETMVC.Controllers
{
    public class EmpolyeesController : Controller
    {
        private readonly MvcDemoDbContext mvcDemoDbContext;

        public EmpolyeesController(MvcDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;

        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = mvcDemoDbContext.Empolyees.ToList();
            return View(employees);

        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfTime = addEmployeeRequest.DateOfTime,
                Department = addEmployeeRequest.Department,
            };
            mvcDemoDbContext.Empolyees.Add(employee);
            mvcDemoDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult View(Guid id)
        {
            var employee = mvcDemoDbContext.Empolyees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                var viewmodel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfTime = employee.DateOfTime,
                    Department = employee.Department,
                };
                return View(viewmodel);
            }
            return RedirectToAction("Index");
        }
    }
}
