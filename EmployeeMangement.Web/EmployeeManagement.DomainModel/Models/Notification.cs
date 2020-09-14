using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Models
{
    public class Notification
    {
        [Key]
        public int Nid { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Action { get; set; }
        [ForeignKey("Departments")]
        public int Did { get; set; }
    }
}
