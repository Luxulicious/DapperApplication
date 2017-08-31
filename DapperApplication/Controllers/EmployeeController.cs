using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DapperApplication.Models;
using DapperApplication.Repository;

namespace DapperApplication.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult GetEmployees()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            return View(employeeRepository.GetEmployees());
        }

        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                //TODO Research modelstates
                if (ModelState.IsValid)
                {
                    EmployeeRepository employeeRepository = new EmployeeRepository();
                    employeeRepository.AddEmployee(employeeModel);
                    ViewBag.Message = $"Succesfully added {employeeModel.Name}.";
                }
                else
                {
                    ViewBag.Message = $"Failed to add {employeeModel.Name}.";
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UpdateEmployee(int id)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            EmployeeModel employeeModel = employeeRepository.GetEmployees().Find(Employee => Employee.Id == id);
            return View(employeeModel);
        }

        public ActionResult UpdateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                EmployeeRepository employeeRepository = new EmployeeRepository();
                employeeRepository.UpdateEmployee(employeeModel);
                return RedirectToAction("GetEmployees");
            }
            catch
            {
                return View();
            }
            return View();
        }

        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                EmployeeRepository employeeRepository = new EmployeeRepository();
                if (employeeRepository.DeleteEmployee(id))
                {
                    ViewBag.AlertMsg = "Employee deleted succesfully";
                }
                return RedirectToAction("GetEmployees");
            }
            catch
            {
                return RedirectToAction("GetEmployees");
            }
        }
    }
}