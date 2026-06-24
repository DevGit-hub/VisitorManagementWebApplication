using System;
using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models
{
    public class RoleMenu
    {
        [Key]
        public int RoleMenuId { get; set; }

        public string Role { get; set; }

        public int MenuId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}