using Microsoft.AspNet.Identity;
using SoloCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoloCapstone.Controllers
{
    public class ProductionManagerController : Controller
    {
        ApplicationDbContext db;
        public ProductionManagerController()
        {
            db = new ApplicationDbContext();
        }
        // GET: ProductionManager
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductionManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductionManager/Create
        public ActionResult Create()
        {
            ProductionManager productionManager = new ProductionManager();
            return View(productionManager);
        }

        // POST: ProductionManager/Create
        [HttpPost]
        public ActionResult Create(ProductionManager productionManager)
        {
            var CurrentUser = User.Identity.GetUserId();
            try
            {
                productionManager.ApplicationUserId = CurrentUser;
                db.ProductionManagers.Add(productionManager);
                db.SaveChanges();
                return RedirectToAction("Index","Order");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CreateWorkOrder()
        {
            Order order = new Order();
            return View(order);
        }
        [HttpPost]
        public ActionResult CreateWorkOrder(Order order)
        {
            var CurrentUser = User.Identity.GetUserId();
            try
            {
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductionManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductionManager/Edit/5
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

        // GET: ProductionManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductionManager/Delete/5
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
