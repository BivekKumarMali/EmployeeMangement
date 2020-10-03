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
        public int nid { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string action { get; set; }
        [ForeignKey("Departments")]
        public int did { get; set; }

        public IsRead isRead { get; set; }
    }
}
