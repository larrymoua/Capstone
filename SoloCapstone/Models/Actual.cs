using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoloCapstone.Models
{
    public class Actual
    {
        [Key]
        public int Id { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public virtual int GoalFK { get; set; }
        [ForeignKey("GoalFK")]
        public virtual Goal Goal { get; set; }
    }
}