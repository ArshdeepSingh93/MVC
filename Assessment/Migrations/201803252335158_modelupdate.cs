namespace Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "ContactNumber", c => c.Long(nullable: false));
            DropColumn("dbo.Logs", "ActionID");
            DropColumn("dbo.Logs", "Created_by");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "Created_by", c => c.String());
            AddColumn("dbo.Logs", "ActionID", c => c.String());
            AlterColumn("dbo.Employees", "ContactNumber", c => c.String(nullable: false));
        }
    }
}
