using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class InventoryModel
    {
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        public int? Quantity { get; set; }
        public string ItemPartNumber { get; set; }
        public string Description { get; set; }
        public string OrderStatus { get; set; }
        [Display(Name = "Market Value")]
        public int MarketValue { get; set; }
        public int ImageId { get; set; }
        [DisplayName("Upload File")]
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}