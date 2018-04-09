namespace Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogTableUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logs", "EventType", c => c.String());
            AlterColumn("dbo.Logs", "Created_by", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logs", "Created_by", c => c.String(nullable: false));
            AlterColumn("dbo.Logs", "EventType", c => c.String(nullable: false));
        }
    }
}
