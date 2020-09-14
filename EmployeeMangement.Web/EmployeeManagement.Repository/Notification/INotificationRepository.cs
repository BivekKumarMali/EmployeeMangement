using EmployeeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repository
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNotifications();
        IEnumerable<Notification> GetNotificationsByDid(int Did);


        Task AddDepartmentNotification();
        Task EditDepartmentNotification();
        Task AddEmployeeNotification(string name, int Did);
        Task EditEmployeeNotification(string name, int Did);
    }
}
