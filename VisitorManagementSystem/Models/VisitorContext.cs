using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VisitorManagementSystem.Models
{
    public class VisitorContext : DbContext
    {
        public VisitorContext()
            : base("VisitorDBConnection")
        {

        }

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<User> Users {get; set;}

        public DbSet<Menu> Menus { get; set; }

        public DbSet<RoleMenu> RoleMenus { get; set; }
    }
}