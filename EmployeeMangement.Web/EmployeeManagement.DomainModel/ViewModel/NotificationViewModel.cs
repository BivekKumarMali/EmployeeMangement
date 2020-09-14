using EmployeeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.ViewModel
{
    public class NotificationViewModel
    {
        public IEnumerable<Notification> Notifications { get; set; }
        public int Count { get; set; }
    }
}
