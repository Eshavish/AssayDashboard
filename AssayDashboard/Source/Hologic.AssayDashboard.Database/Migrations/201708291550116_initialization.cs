namespace Hologic.AssayDashboard.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssayTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AssayName = c.String(nullable: false),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(nullable: false),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CurveFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Tag = c.String(),
                        IsGolden = c.Boolean(nullable: false),
                        FullFileName = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        AssayDBID = c.Int(nullable: false),
                        ModifiedBy = c.String(nullable: false),
                        Data = c.Binary(nullable: false),
                        AssayType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AssayTypes", t => t.AssayType_ID)
                .Index(t => t.AssayType_ID);
            
            CreateTable(
                "dbo.ReportFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        AssayTypeId = c.Int(nullable: false),
                        ReportTypeId = c.Int(nullable: false),
                        MajorVersion = c.Int(nullable: false),
                        MinorVersion = c.Int(nullable: false),
                        ServicePackNumber = c.Int(nullable: false),
                        BuildNumber = c.Int(nullable: false),
                        FileContent = c.Binary(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssayTypes", t => t.AssayTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ReportTypes", t => t.ReportTypeId, cascadeDelete: true)
                .Index(t => t.AssayTypeId)
                .Index(t => t.ReportTypeId);
            
            CreateTable(
                "dbo.ReportTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReportName = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportFiles", "ReportTypeId", "dbo.ReportTypes");
            DropForeignKey("dbo.ReportFiles", "AssayTypeId", "dbo.AssayTypes");
            DropForeignKey("dbo.CurveFiles", "AssayType_ID", "dbo.AssayTypes");
            DropIndex("dbo.ReportFiles", new[] { "ReportTypeId" });
            DropIndex("dbo.ReportFiles", new[] { "AssayTypeId" });
            DropIndex("dbo.CurveFiles", new[] { "AssayType_ID" });
            DropTable("dbo.ReportTypes");
            DropTable("dbo.ReportFiles");
            DropTable("dbo.CurveFiles");
            DropTable("dbo.AssayTypes");
        }
    }
}
