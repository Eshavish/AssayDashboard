namespace Hologic.AssayDashboard.Database.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Hologic.AssayDashboard.Database.AssayDashboardContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Hologic.AssayDashboard.Database.AssayDashboardContext context)
        {
            //  This method will be called after migrating to the latest version.
        }
    }
}
