namespace Hologic.AssayDashboard.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAssayTypesTableToAddShortName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssayTypes", "AssayShortName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssayTypes", "AssayShortName");
        }
    }
}
