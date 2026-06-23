using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }

        public string DisplayName { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public int? ParentMenuId { get; set; }

        public bool IsActive { get; set; }
    }
}