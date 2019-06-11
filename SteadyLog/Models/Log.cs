using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SteadyLog.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedOn { get; set; }

        public string Comment { get; set; }
    }
}
