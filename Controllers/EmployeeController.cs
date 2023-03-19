using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreCURDApp.Data;
using AspnetCoreCURDApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspnetCoreCURDApp.Controllers
{
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly AppDBContext _context;
        public EmployeeController(AppDBContext context)
        {
            this._context = context;
        }
        //index
        [HttpGet]
        // private readonly ILogger<EmployeeController> _logger;

        // public EmployeeController(ILogger<EmployeeController> logger)
        // {
        //     _logger = logger;
        // }

        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            List<EmployeeViewModal> employeeList = new List<EmployeeViewModal>();

            if (employees != null)
            {
                foreach (var employee in employees)
                {
                    var employeeViewModal = new EmployeeViewModal()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    employeeList.Add(employeeViewModal);
                }
                return View(employeeList);
            }
            return View(employeeList);
        }

        //create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("create")]
        public IActionResult Create(EmployeeViewModal AppDBContext)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        FirstName = AppDBContext.FirstName,
                        LastName = AppDBContext.LastName,
                        DateOfBirth = AppDBContext.DateOfBirth,
                        Email = AppDBContext.Email,
                        Salary = AppDBContext.Salary
                    };
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Created Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Please fill all the fields";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            try
            {
                var employee = _context.Employees.SingleOrDefault(x => x.Id == id);
                if (employee != null)
                {
                    var employeeView = new EmployeeViewModal()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    return View(employeeView);
                }
                else
                {
                    TempData["ErrorMessage"] = $"Employee details not available with th Id: {id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }


        [HttpPost("edit/{id}")]
        public IActionResult Edit(EmployeeViewModal model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        Salary = model.Salary
                    };
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Updated Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Model is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var employee = _context.Employees.SingleOrDefault(x => x.Id == id);
                if (employee != null)
                {
                    var employeeView = new EmployeeViewModal()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    return View(employeeView);
                }
                else
                {
                    TempData["ErrorMessage"] = $"Employee details not available with th Id: {id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(EmployeeViewModal model)
        {
            try
            {
                var employee = _context.Employees.SingleOrDefault(x => x.Id == model.Id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Deleted Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = $"Employee details not available with th Id: {model.Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}