namespace VisitorManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditFields : DbMigration
    {
        public override void Up()
        {
            
            AddColumn("dbo.RoleMenus", "CreatedBy", c => c.String());
            AddColumn("dbo.RoleMenus", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.RoleMenus", "UpdatedBy", c => c.String());
            AddColumn("dbo.RoleMenus", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.RoleMenus", "DeletedBy", c => c.String());
            AddColumn("dbo.RoleMenus", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Users", "UpdatedBy", c => c.String());
            AddColumn("dbo.Users", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Users", "DeletedBy", c => c.String());
            AddColumn("dbo.Users", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Visitors", "CreatedBy", c => c.String());
            AddColumn("dbo.Visitors", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Visitors", "UpdatedBy", c => c.String());
            AddColumn("dbo.Visitors", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Visitors", "DeletedBy", c => c.String());
            AddColumn("dbo.Visitors", "DeletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visitors", "DeletedDate");
            DropColumn("dbo.Visitors", "DeletedBy");
            DropColumn("dbo.Visitors", "UpdatedDate");
            DropColumn("dbo.Visitors", "UpdatedBy");
            DropColumn("dbo.Visitors", "CreatedDate");
            DropColumn("dbo.Visitors", "CreatedBy");
            DropColumn("dbo.Users", "DeletedDate");
            DropColumn("dbo.Users", "DeletedBy");
            DropColumn("dbo.Users", "UpdatedDate");
            DropColumn("dbo.Users", "UpdatedBy");
            DropColumn("dbo.RoleMenus", "DeletedDate");
            DropColumn("dbo.RoleMenus", "DeletedBy");
            DropColumn("dbo.RoleMenus", "UpdatedDate");
            DropColumn("dbo.RoleMenus", "UpdatedBy");
            DropColumn("dbo.RoleMenus", "CreatedDate");
            DropColumn("dbo.RoleMenus", "CreatedBy");
            
        }
    }
}
