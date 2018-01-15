namespace Hologic.AssayDashboard.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHashColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportFiles", "HashValue", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReportFiles", "HashValue");
        }
    }
}
