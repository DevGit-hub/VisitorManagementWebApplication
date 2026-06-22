namespace VisitorManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Visitors", "NIC", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Visitors", "NIC", c => c.String());
        }
    }
}
