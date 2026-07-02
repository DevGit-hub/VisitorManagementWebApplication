using System;
using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}