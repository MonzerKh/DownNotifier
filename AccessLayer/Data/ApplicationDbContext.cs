using AccessLayer.FluentConfig;
using Microsoft.EntityFrameworkCore;
using ModelsLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region Data Set Tables
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<TargetApplication> TargetApplications { get; set; }
        public DbSet<AppCheckHistory> AppCheckHistories { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfiguration(new SystemUsersFluent());
            modelBuilder.ApplyConfiguration(new TargetApplicationFluent());
            modelBuilder.ApplyConfiguration(new AppCheckHistoryFluent());

        }
    }
}
