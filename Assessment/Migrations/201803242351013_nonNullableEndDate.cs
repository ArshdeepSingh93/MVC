namespace Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nonNullableEndDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "EndDate", c => c.DateTime());
        }
    }
}
