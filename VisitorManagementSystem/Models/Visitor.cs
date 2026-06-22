using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisitorManagementSystem.Models
{
    public class Visitor
    {


        [Key]
        public int VisitorID { get; set; }

        public string FullName {  get; set; }

        [Required(ErrorMessage = "NIC is required")]
        [RegularExpression(@"^([0-9]{9}[vVxX]|[0-9]{12})$",
            ErrorMessage = "Enter a valid NIC")]
        public string NIC { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^07\d{8}$",
            ErrorMessage = "Enter avalid 10-digit phone number")]
        public string Phone { get; set; }

        public string Purpose { get; set; }
        public string PersonToVisit { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Status { get; set; }

        public bool IsDeleted { get; set; }


    }
}