using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Models
{
    public class Department
    {
        [Key]
        public int Did { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        public string RoleId;
    }
}
