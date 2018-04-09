namespace Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ContactNumber = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Position = c.String(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        EmploymentStatus = c.String(nullable: false),
                        ShiftTimings = c.String(nullable: false),
                        ImageFileName = c.String(),
                        FavColor = c.String(),
                        ParentId = c.Int(),
                        DepartmenId = c.Int(nullable: false),
                        Department_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.ParentId)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .Index(t => t.ParentId)
                .Index(t => t.Department_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Employees", "ParentId", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "Department_Id" });
            DropIndex("dbo.Employees", new[] { "ParentId" });
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
