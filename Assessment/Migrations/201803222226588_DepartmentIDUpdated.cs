namespace Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentIDUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "Department_Id" });
            RenameColumn(table: "dbo.Employees", name: "Department_Id", newName: "DepartmentId");
            AlterColumn("dbo.Employees", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "DepartmentId");
            AddForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
            DropColumn("dbo.Employees", "DepartmenId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "DepartmenId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            AlterColumn("dbo.Employees", "DepartmentId", c => c.Int());
            RenameColumn(table: "dbo.Employees", name: "DepartmentId", newName: "Department_Id");
            CreateIndex("dbo.Employees", "Department_Id");
            AddForeignKey("dbo.Employees", "Department_Id", "dbo.Departments", "Id");
        }
    }
}
