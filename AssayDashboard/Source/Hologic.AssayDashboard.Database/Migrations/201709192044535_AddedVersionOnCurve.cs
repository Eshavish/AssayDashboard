namespace Hologic.AssayDashboard.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVersionOnCurve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurveFiles", "AssayVersion", c => c.String());
            AddColumn("dbo.CurveFiles", "SoftwareVersion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CurveFiles", "SoftwareVersion");
            DropColumn("dbo.CurveFiles", "AssayVersion");
        }
    }
}
