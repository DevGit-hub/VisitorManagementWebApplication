namespace VisitorManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleMenuTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleMenus",
                c => new
                    {
                        RoleMenuId = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                        MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleMenuId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoleMenus");
        }
    }
}
