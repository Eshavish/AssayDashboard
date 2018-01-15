using Hologic.AssayDashboard.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Database
{
    public class AssayDashboardContext : DbContext
    {
        public DbSet<AssayType> AssayType { get; set; }
        public DbSet<CurveFile> CurveFile { get; set; }
        public DbSet<ReportFile> ReportFile { get; set; }
        public DbSet<ReportType> ReportType { get; set; }

        public AssayDashboardContext() : base("AssayDatabase")
        {
            //Console.WriteLine();
            //Console.WriteLine(Database.Connection.ConnectionString) ;

            System.Data.Entity.Database.SetInitializer(new AssayDashboardDatabaseInitializer());
        }

        public void InitializeDatabase()
        {
            if (!Database.Exists())
            {
                Database.Initialize(true);
            }
        }
    }
}
