using System;
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

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}