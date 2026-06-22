namespace VisitorManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FullName", c => c.String());
            AddColumn("dbo.Users", "NIC", c => c.String());
            AddColumn("dbo.Users", "MustChangePassword", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Role", c => c.String());
            DropColumn("dbo.Users", "MustChangePassword");
            DropColumn("dbo.Users", "NIC");
            DropColumn("dbo.Users", "FullName");
        }
    }
}
