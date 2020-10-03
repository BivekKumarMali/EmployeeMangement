﻿using EmployeeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repository
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNotifications();
        void SendNotification();


        Task AddDepartmentNotification();
        Task EditDepartmentNotification();
        void DeleteDepartmentNotification();
        Task AddEmployeeNotification(Employee employee);
        Task EditEmployeeNotification(Employee employee);
        Task DeleteEmployeeNotification(Employee employee);
        void IsReadNotification(int nid, string userId);
    }
}
