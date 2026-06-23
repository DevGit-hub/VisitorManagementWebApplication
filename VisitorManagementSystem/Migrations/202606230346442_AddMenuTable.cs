namespace VisitorManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMenuTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        ParentMenuId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MenuId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Menus");
        }
    }
}
