using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Models
{ 
    public class Employee
    {
        [Key]
        public int Eid { get; set; }
        [StringLength(20)]
        [Required]
        [Display(Name = "Frist Name")]
        public string Name { get; set; }
        [StringLength(20)]
        [Required]
        [Display(Name = "Last Name")]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [StringLength(100)]
        [Required]
        public string Address { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public long ContactNumber { get; set; }
        [Required]
        [ForeignKey("Department")]
        public int Did { get; set; }

        public string UserId { get; set; }

        public Department Department { get; set; }

    }
}
