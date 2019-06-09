using SteadyLog.Data;
using SteadyLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteadyLog.Services
{
    public class ActivityService
    {
        private readonly ApplicationDbContext _context;
        public ActivityService(ApplicationDbContext context)
        {
            _context = context;
        }


        public List<Activity> Activities { get; set; }

        public List<Activity> GetActivities()
        {
            throw new NotImplementedException();
        }

        public int GetNextActivityId()
        {
            return _context.Activities.Last().Id + 1;
        }
    }
}
