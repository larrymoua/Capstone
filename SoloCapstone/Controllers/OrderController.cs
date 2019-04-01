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
using System.IO;
using System.Text;

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
            ViewBag.OrderId = foundOrder.OrderId;
            return View(foundOrder);
        }
        public ActionResult InventoryItemDetails(string id)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("http://localhost:52290/api/Inventory/");
                HttpResponseMessage response = client.GetAsync($"{id}").Result;
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                InventoryModel inventory = JsonConvert.DeserializeObject<InventoryModel>(result);
                return View(inventory);
            }    
        }
        public ActionResult OrderMaterials(int id)
        {
            var foundOrder = db.orders.Find(id);
            var Product = db.products.Where(c => c.OrderId == foundOrder.OrderId).ToList();

            return View(Product);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            Order newOrder = new Order();
            return View(newOrder);
        }
        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Order newOrder)
        {

            try
            {
                db.orders.Add(newOrder);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult CreateCoaxialCable(int orderId)
        {
            CoaxialCable coaxialCable = new CoaxialCable();
            coaxialCable.OrderId = orderId;
            return View(coaxialCable);
        }
        // POST: Order/Create
        [HttpPost]
        public ActionResult CreateCoaxialCable(CoaxialCable coaxialCable)
        {
            try
            {
                db.products.Add(coaxialCable);
                db.SaveChanges();
                
                return RedirectToAction("OrderMaterials", "Order", new { id = coaxialCable.OrderId });
            }
            catch
            {
                return RedirectToAction("OrderMaterials", "Order", new { id = coaxialCable.OrderId });
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
                return RedirectToAction("Index");
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            var findOrder = db.orders.Where(i => i.OrderId ==id).Single();
            return View(findOrder);
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Order order)
        {
            var findOrder = db.orders.Where(i => i.OrderId == id).Single();
            try
            {
                db.orders.Remove(findOrder);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
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
        public ActionResult DeleteInventoryItem(string id)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52290/api/Inventory/");
               var response = client.DeleteAsync($"{id}").Result;
        
                response.EnsureSuccessStatusCode();
            }
            Image image = db.Images.Where(i => i.ItemName == id).SingleOrDefault();
            db.Images.Remove(image);
            db.SaveChanges();

            return RedirectToAction("ShowInventory");
        }
        public ActionResult CreateInventoryItem()
        {
            InventoryModel model = new InventoryModel(); 
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateInventoryItem(InventoryModel inventory)
        {
                string fileName = Path.GetFileNameWithoutExtension(inventory.ImageFile.FileName);
                string extension = Path.GetExtension(inventory.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                inventory.ImagePath = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                inventory.ImageFile.SaveAs(fileName);

                 using (HttpClient client = new HttpClient())
                {
                    inventory.ImageFile = null;
                    client.BaseAddress = new Uri("http://localhost:52290/");
                    var response = client.PostAsJsonAsync("api/Inventory/", inventory).Result;
                    Image image = new Image { ImagePath = inventory.ImagePath, ItemName = inventory.ItemName };
                    db.Images.Add(image);
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("ShowInventory");

                }
       


        }
        public ActionResult EditInventoryItem(string id)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("http://localhost:52290/api/Inventory/");
                HttpResponseMessage response = client.GetAsync($"{id}").Result;
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var inventoryModel = JsonConvert.DeserializeObject<InventoryModel>(result);
                return View(inventoryModel);
            }
     
        }
        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult EditInventoryItem(InventoryModel inventoryModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(inventoryModel.ImageFile.FileName);
            string extension = Path.GetExtension(inventoryModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            inventoryModel.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            inventoryModel.ImageFile.SaveAs(fileName);
            inventoryModel.ImageFile = null;
            try
            {
                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
                {
                    client.BaseAddress = new Uri("http://localhost:52290/api/Inventory/");
                    HttpResponseMessage response = client.PutAsJsonAsync(client.BaseAddress, inventoryModel).Result;

                    return RedirectToAction("ShowInventory");

                }
            }
            catch
            {
                return RedirectToAction("ShowInventory");
            }
  


        }
        public ActionResult EditProduct(int id)
        {
            var foundProduct = db.products.Find(id);
            return View(foundProduct);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult EditProduct(CoaxialCable coaxialCable)
        {
            var findProduct = db.products.Where(p => p.PartId == coaxialCable.PartId).SingleOrDefault();
            try
            {
             
                findProduct.HeatShrinkQuantity = coaxialCable.HeatShrinkQuantity;
                findProduct.Impedance = coaxialCable.Impedance;
                findProduct.PartName = coaxialCable.PartName;
                findProduct.CableQuantity = coaxialCable.ConnecterQuantity;
                findProduct.AWG = coaxialCable.AWG;
                findProduct.ConnecterQuantity = coaxialCable.ConnecterQuantity;

                db.SaveChanges();

                return RedirectToAction("OrderMaterials", "Order", new { id = findProduct.OrderId });
            }
            catch
            {
                return RedirectToAction("OrderMaterials", "Order", new { id = findProduct.OrderId });
            }
        }
    }
}
