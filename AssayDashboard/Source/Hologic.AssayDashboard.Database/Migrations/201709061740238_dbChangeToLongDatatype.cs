namespace Hologic.AssayDashboard.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChangeToLongDatatype : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CurveFiles", "AssayType_ID", "dbo.AssayTypes");
            DropForeignKey("dbo.ReportFiles", "AssayTypeId", "dbo.AssayTypes");
            DropForeignKey("dbo.ReportFiles", "ReportTypeId", "dbo.ReportTypes");
            DropIndex("dbo.CurveFiles", new[] { "AssayType_ID" });
            DropIndex("dbo.ReportFiles", new[] { "AssayTypeId" });
            DropIndex("dbo.ReportFiles", new[] { "ReportTypeId" });
            DropPrimaryKey("dbo.AssayTypes");
            DropPrimaryKey("dbo.CurveFiles");
            DropPrimaryKey("dbo.ReportFiles");
            DropPrimaryKey("dbo.ReportTypes");
            AlterColumn("dbo.AssayTypes", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.AssayTypes", "TypeId", c => c.Long(nullable: false));
            AlterColumn("dbo.CurveFiles", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.CurveFiles", "AssayDBID", c => c.Long(nullable: false));
            AlterColumn("dbo.CurveFiles", "AssayType_ID", c => c.Long());
            AlterColumn("dbo.ReportFiles", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.ReportFiles", "AssayTypeId", c => c.Long(nullable: false));
            AlterColumn("dbo.ReportFiles", "ReportTypeId", c => c.Long(nullable: false));
            AlterColumn("dbo.ReportTypes", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.AssayTypes", "ID");
            AddPrimaryKey("dbo.CurveFiles", "ID");
            AddPrimaryKey("dbo.ReportFiles", "Id");
            AddPrimaryKey("dbo.ReportTypes", "Id");
            CreateIndex("dbo.CurveFiles", "AssayType_ID");
            CreateIndex("dbo.ReportFiles", "AssayTypeId");
            CreateIndex("dbo.ReportFiles", "ReportTypeId");
            AddForeignKey("dbo.CurveFiles", "AssayType_ID", "dbo.AssayTypes", "ID");
            AddForeignKey("dbo.ReportFiles", "AssayTypeId", "dbo.AssayTypes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ReportFiles", "ReportTypeId", "dbo.ReportTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportFiles", "ReportTypeId", "dbo.ReportTypes");
            DropForeignKey("dbo.ReportFiles", "AssayTypeId", "dbo.AssayTypes");
            DropForeignKey("dbo.CurveFiles", "AssayType_ID", "dbo.AssayTypes");
            DropIndex("dbo.ReportFiles", new[] { "ReportTypeId" });
            DropIndex("dbo.ReportFiles", new[] { "AssayTypeId" });
            DropIndex("dbo.CurveFiles", new[] { "AssayType_ID" });
            DropPrimaryKey("dbo.ReportTypes");
            DropPrimaryKey("dbo.ReportFiles");
            DropPrimaryKey("dbo.CurveFiles");
            DropPrimaryKey("dbo.AssayTypes");
            AlterColumn("dbo.ReportTypes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ReportFiles", "ReportTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.ReportFiles", "AssayTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.ReportFiles", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CurveFiles", "AssayType_ID", c => c.Int());
            AlterColumn("dbo.CurveFiles", "AssayDBID", c => c.Int(nullable: false));
            AlterColumn("dbo.CurveFiles", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AssayTypes", "TypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.AssayTypes", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ReportTypes", "Id");
            AddPrimaryKey("dbo.ReportFiles", "Id");
            AddPrimaryKey("dbo.CurveFiles", "ID");
            AddPrimaryKey("dbo.AssayTypes", "ID");
            CreateIndex("dbo.ReportFiles", "ReportTypeId");
            CreateIndex("dbo.ReportFiles", "AssayTypeId");
            CreateIndex("dbo.CurveFiles", "AssayType_ID");
            AddForeignKey("dbo.ReportFiles", "ReportTypeId", "dbo.ReportTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReportFiles", "AssayTypeId", "dbo.AssayTypes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CurveFiles", "AssayType_ID", "dbo.AssayTypes", "ID");
        }
    }
}
