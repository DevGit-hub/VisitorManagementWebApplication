using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models
{
    public class RoleMenu
    {
        [Key]
        public int RoleMenuId { get; set; }

        public string Role { get; set; }

        public int MenuId { get; set; }
    }
}