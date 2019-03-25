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
        public double Quantity { get; set; }

    }
}