namespace VisitorManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Role", c => c.String());
            DropColumn("dbo.Users", "FullName");
            DropColumn("dbo.Users", "NIC");
            DropColumn("dbo.Users", "MustChangePassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "MustChangePassword", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "NIC", c => c.String());
            AddColumn("dbo.Users", "FullName", c => c.String());
            DropColumn("dbo.Users", "Role");
        }
    }
}
