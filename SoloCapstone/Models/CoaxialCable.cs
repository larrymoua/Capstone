using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class CoaxialCable
    {
        [Key]
        public int PartId { get; set; }
        public string PartName { get; set; }
        public int AWG { get; set; }
        public string Impedance { get; set; }
        public ProductStatus status { get; set; }
        public double CableQuantity { get; set; }
        public double ConnecterQuantity { get; set; }
        public double HeatShrinkQuantity { get; set; }
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