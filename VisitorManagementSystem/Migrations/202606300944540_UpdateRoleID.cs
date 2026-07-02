namespace VisitorManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoleID : DbMigration
    {
        public override void Up()
        {
            
            
            CreateIndex("dbo.RoleMenus", "MenuId");
            AddForeignKey("dbo.RoleMenus", "MenuId", "dbo.Menus", "MenuId", cascadeDelete: true);
            AddForeignKey("dbo.RoleMenus", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoleMenus", "Role", c => c.String());
            DropForeignKey("dbo.RoleMenus", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoleMenus", "MenuId", "dbo.Menus");
            DropIndex("dbo.RoleMenus", new[] { "MenuId" });
            DropIndex("dbo.RoleMenus", new[] { "RoleId" });
            DropColumn("dbo.RoleMenus", "RoleId");
        }
    }
}
