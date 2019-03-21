using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public ProductStatus status { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
     
        public virtual Order Orders { get; set; }
    }
    public enum ProductStatus
    {
        Stage1,
        Stage2,
        Stage3,
        Finished,
        
    }
}