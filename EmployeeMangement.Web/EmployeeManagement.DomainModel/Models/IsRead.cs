using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Models
{
    public class IsRead
    {
        [Key]
        public int rid { get; set; }
        [ForeignKey("AspNetUsers")]
        public string userId { get; set; }
        [ForeignKey("Notifications")]
        public int nid { get; set; }

    }
}
