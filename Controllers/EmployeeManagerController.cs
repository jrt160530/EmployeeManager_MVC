using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager_MVC.Models;
using EmployeeManager_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager_MVC.Controllers
{
    
    [Authorize]
    public class EmployeeManagerController : Controller
    {
        private AppDbContext _dbcontext = null;

        public EmployeeManagerController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        // can also set the Authorize attribute for individual methods.
        // also can set [AllowAnonymous] to give unauthorized access 
        // to indivudal methods in cases the class is authorized attributed.
        // [Authorize(Roles="Manager")]
        public IActionResult List()
        {
            List<Employee> employees = (from e in _dbcontext.Employees
                                        orderby e.LastName ascending
                                        select e).ToList();
            return View(employees);
        }

        public IActionResult Insert()
        {
            FillCountries();
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="Manager")]
        public IActionResult Insert(Employee model)
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                _dbcontext.Employees.Add(model);
                _dbcontext.SaveChanges();
                ViewBag.Message = "Employee inserted successfully";
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Employee model)
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                _dbcontext.Employees.Update(model);
                _dbcontext.SaveChanges();
                ViewBag.Message = "Employee updated successfully";
            }
            return View(model);
        }

        public IActionResult Update(int id)
        {
            FillCountries();
            Employee model = _dbcontext.Employees.Find(id);
            return View(model);
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            Employee model = _dbcontext.Employees.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int employeeID)
        {
           // Employee model = employee;
            Employee model = _dbcontext.Employees.Find(employeeID);
            _dbcontext.Employees.Remove(model);
            _dbcontext.SaveChanges();
            TempData["Message"] = "Employee deleted successfully";
            return RedirectToAction("List");
        }

        private void FillCountries()
        {
            List<SelectListItem> countries = (from c in _dbcontext.Countries
                                              orderby c.Name ascending
                                              select new SelectListItem()
                                              { Text = c.Name, Value = c.Name }).ToList();
            ViewBag.Countries = countries;
        }
    }
}


