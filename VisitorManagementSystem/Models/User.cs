using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisitorManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        [Required(ErrorMessage = "NIC is required")]
        [RegularExpression(@"^([0-9]{9}[vVxX]|[0-9]{12})$",
           ErrorMessage = "Enter a valid NIC")]
        public string NIC { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }


        public string Role { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}