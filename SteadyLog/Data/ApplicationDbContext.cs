using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SteadyLog.Models;
using SteadyLog.Models.LogViewModels;

namespace SteadyLog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Log> Logs { get; set; }
        //public DbSet<SteadyLog.Models.LogViewModels.LogViewModel> LogViewModel { get; set; }
        //public DbSet<SteadyLog.Models.LogViewModels.LogSaveViewModel> LogSaveViewModel { get; set; }
    }
}
