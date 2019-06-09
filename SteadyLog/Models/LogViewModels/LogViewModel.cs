using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteadyLog.Models.LogViewModels
{
    public class LogViewModel
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string ActivityTitle { get; set; }
        public int Amount { get; set; }
    }
}
