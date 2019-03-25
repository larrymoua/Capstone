using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SoloCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace SoloCapstone.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db;
        public OrderController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Order
        public ActionResult Index()
        {
            var Orders = db.orders.Select(e => e).ToList();
            Orders = Orders.OrderBy(o => o.DueDate).ToList();
            return View(Orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            var foundOrder = db.orders.Find(id);

            if(foundOrder.OrderStatus.ToString().Equals("OrderConfirmed"))
            {
                ViewBag.OrderStatus = "25";
            }
            else if (foundOrder.OrderStatus.ToString().Equals("BeingPrepared"))
            {
                ViewBag.OrderStatus = "65";
            }
            else
            {
                ViewBag.OrderStatus = "100";
            }
            return View(foundOrder);
        }
        public ActionResult OrderMaterials(int id)
        {
            var foundOrder = db.orders.Find(id);
            var Product = foundOrder.coaxialCables.Select(c => c).ToList();

            return View(Product);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            var foundOrder = db.orders.Find(id);
            return View(foundOrder);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(Order order)
        {
            var CurrentUser = User.Identity.GetUserId();
            var foundOrder = db.orders.Find(order.OrderId);
            try
            {
                foundOrder.OrderStatus = order.OrderStatus;
                if (!foundOrder.DueDate.Equals(order.DueDate) && User.IsInRole("ProductionManager"))
                {
                    foundOrder.DueDate = order.DueDate;
                }
                if (User.IsInRole("Employee"))
                {
                    var foundWorker = db.Employees.Where(e => e.ApplicationUserId == CurrentUser).Single();
                    foundOrder.CurrentlyWorkingOn = foundWorker.FirstName;
                }
              
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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
        public ActionResult ShowInventory()
        {
            IList<InventoryModel> inventories = new List<InventoryModel>();

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("http://localhost:52290/api/Inventory/");
                HttpResponseMessage response = client.GetAsync("").Result;
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                inventories = JsonConvert.DeserializeObject<List<InventoryModel>>(result);

            }
            return View(inventories);
        }

    }
}
