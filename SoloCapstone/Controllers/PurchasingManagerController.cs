using Microsoft.AspNet.Identity;
using SoloCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoloCapstone.Controllers
{
    public class PurchasingManagerController : Controller
    {
        private ApplicationDbContext db;
        public PurchasingManagerController()
        {
            db = new ApplicationDbContext();
        }
        // GET: PurchasingManager
        public ActionResult Index()
        {
            return View();
        }

        // GET: PurchasingManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PurchasingManager/Create
        public ActionResult Create()
        {
            PurchasingManager createPurchasingManager = new PurchasingManager();
;            return View(createPurchasingManager);
        }

        // POST: PurchasingManager/Create
        [HttpPost]
        public ActionResult Create(PurchasingManager createPurchasingManager)
        {
            var CurrentUser = User.Identity.GetUserId();
            try
            {
                createPurchasingManager.ApplicationUserId = CurrentUser;
                db.PurchasingManagers.Add(createPurchasingManager);
                db.SaveChanges();

                return RedirectToAction("Index","Order");
            }
            catch
            {
                return View();
            }
        }

        // GET: PurchasingManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PurchasingManager/Edit/5
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

        // GET: PurchasingManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PurchasingManager/Delete/5
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
