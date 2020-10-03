using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private AppDbContext _context { get; set; }
        private IHubContext<SignalServer> _hubContext;

        public NotificationRepository(
            AppDbContext context, IHubContext<SignalServer> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        

        public IEnumerable<Notification> GetNotifications()
        {
            var notifications = (from notification in _context.Notifications
                                 join isread in _context.IsReads
                                 on notification.nid equals isread.nid
                                 select new Notification
                                 {
                                     nid = notification.nid,
                                     action = notification.action,
                                     date = notification.date,
                                     did = notification.did,
                                     name = notification.name,
                                     isRead = isread

                                 }).ToList();
            return notifications;
        }

        public void SendNotification()
        {
            var result = GetNotifications();
            _hubContext.Clients.All.SendAsync("transferchartdata", result);
        }
        public async Task AddDepartmentNotification()
        {
            Notification notification = new Notification
            {
                name = "Department",
                action = "Added",
                date = DateTime.Now.ToString(),
                did = 0
            };
             AddNotification(notification);
        }

        public async Task EditDepartmentNotification()
        {
            Notification notification = new Notification
            {
                name = "Department",
                action = "Edit",
                date = DateTime.Now.ToString(),
                did = 0
            };
             AddNotification(notification);
        }

        public async Task AddEmployeeNotification(Employee employee)
        {
            Notification notification = new Notification
            {
                name = employee.Name +" "+ employee.Surname,
                action = "Added",
                date = DateTime.Now.ToString(),
                did = employee.Did
            };
             AddNotification(notification);
        }

        public async Task EditEmployeeNotification(Employee employee)
        {
            Notification notification = new Notification
            {
                name = employee.Name + " " + employee.Surname,
                action = "Edit",
                date = DateTime.Now.ToString(),
                did = employee.Did
            };
             AddNotification(notification);
        }

        public void DeleteDepartmentNotification()
        {
            Notification notification = new Notification
            {
                name = "Department",
                action = "Delete",
                date = DateTime.Now.ToString(),
                did = 0
            };
            AddNotification(notification);
        }

        public Task DeleteEmployeeNotification(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void IsReadNotification(int nid, string userId)
        {
            IsRead read = new IsRead
            {
                nid = nid,
                userId = userId
            };
            _context.IsReads.Add(read);
            _context.SaveChanges();
        }

        public void AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            _hubContext.Clients.All.SendAsync("displayNotification");
            _context.SaveChanges();
        }


        
    }
}
