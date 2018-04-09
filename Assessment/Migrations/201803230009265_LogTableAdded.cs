namespace Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogID = c.Guid(nullable: false),
                        EventType = c.String(nullable: false),
                        TableName = c.String(nullable: false),
                        ActionID = c.String(),
                        RecordID = c.String(nullable: false),
                        ColumnName = c.String(nullable: false),
                        OriginalValue = c.String(),
                        NewValue = c.String(),
                        Created_by = c.String(nullable: false),
                        Created_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LogID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
