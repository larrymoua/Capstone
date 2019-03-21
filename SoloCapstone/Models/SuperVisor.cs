using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class SuperVisor
    {
        [Key]
        public int SuperVisorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}