using EmployeeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repository
{
    interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotifications();
        Task<IEnumerable<Notification>> GetNotificationsByDid();

        void AddNotification(Notification notification);
    }
}
