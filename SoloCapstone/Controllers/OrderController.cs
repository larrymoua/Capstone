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
using System.Net.Http.Formatting;
using System.Data;
using System.Data.Entity;


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
            return View();
        }
        public JsonResult GetAllOrders()
        {
            List<Order> orders = db.orders.Select(e => e).ToList();
            orders = orders.OrderBy(o => o.DueDate).ToList();
            return Json(orders, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllInventoryItems()
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
            return Json(inventories, JsonRequestBehavior.AllowGet);
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
        public ActionResult DeleteInventoryItem()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52290/");
                var response = client.DeleteAsync("api/Inventory/");
            }
            return View("ShowInventory");
        }
        public ActionResult CreateInventoryItem()
        {
            InventoryModel model = new InventoryModel(); 
            return View("CreateInventoryItem", model);
        }
        [HttpPost]
        public ActionResult CreateInventoryItem(InventoryModel inventory)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:52290/");
                    var response = client.PostAsJsonAsync("api/Inventory/", inventory).Result;
                    return RedirectToAction("ShowInventory");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("ShowInventory");
            }


        }
        public ActionResult EditInventoryItem(string itemPartNumber)
        {
            InventoryModel inventoryModel = new InventoryModel { ItemPartNumber = itemPartNumber };
            return View(inventoryModel);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult EditInventoryItem(InventoryModel inventoryModel)
        {

            try
            {
  

                return RedirectToAction("ShowInventory");
            }
            catch
            {
                return RedirectToAction("ShowInventory");
            }
        }
    }
}
