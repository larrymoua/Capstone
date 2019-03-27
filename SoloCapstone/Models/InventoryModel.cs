using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class InventoryModel
    {
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string ItemPartNumber { get; set; }
        public string Description { get; set; }
        public string OrderStatus { get; set; }
    }
}