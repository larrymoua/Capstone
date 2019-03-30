using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class Image
    {
        [Key]
        public string ItemName { get; set; }
        public string ImagePath { get; set; }

    }
}
