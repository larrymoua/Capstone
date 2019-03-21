using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate {get; set; }
        public string OrderConfirmationNumber { get; set; }
        public string CustomerName { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string CurrentlyWorkingOn { get; set; }

        public IEnumerable <CoaxialCable> coaxialCables { get; set; }
    }
    public enum OrderStatus
    {
        OrderConfirmed,
        BeingPrepared,
        Shipped

    }
}