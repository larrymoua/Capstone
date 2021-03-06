﻿using Microsoft.AspNet.Identity;
using SoloCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoloCapstone.Controllers
{
    public class EmployeeController : Controller
    {
        ApplicationDbContext db;
        public EmployeeController()
        {
           db = new ApplicationDbContext();
        }
        // GET: Employee
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Order");
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            Employee employee = new Employee();
            return View(employee);
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            var CurrentUser = User.Identity.GetUserId();
            try
            {
                employee.ApplicationUserId = CurrentUser;
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index","Order");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View("Edit", "Order", id);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
