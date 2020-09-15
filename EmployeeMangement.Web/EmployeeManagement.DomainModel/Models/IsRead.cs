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
        public int Rid { get; set; }
        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        [ForeignKey("Notifications")]
        public int Nid { get; set; }

    }
}
