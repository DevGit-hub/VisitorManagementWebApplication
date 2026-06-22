namespace VisitorManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdateNIC : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "NIC", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "NIC", c => c.String());
        }
    }
}
