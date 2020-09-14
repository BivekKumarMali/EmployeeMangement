using EmployeeManagement.Web.Models;
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

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddNotification(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return _context.Notifications.ToList();
        }

        public IEnumerable<Notification> GetNotificationsByDid(int Did)
        {
            return _context.Notifications.ToList().Where(x => x.Did == Did);
        }

        public async Task AddDepartmentNotification()
        {
            Notification notification = new Notification
            {
                Name = "Department",
                Action = "Added",
                Date = DateTime.Now.ToString(),
                Did = 0
            };
            await AddNotification(notification);
        }

        public async Task EditDepartmentNotification()
        {
            Notification notification = new Notification
            {
                Name = "Department",
                Action = "Edit",
                Date = DateTime.Now.ToString(),
                Did = 0
            };
            await AddNotification(notification);
        }

        public async Task AddEmployeeNotification(string name, int Did)
        {
            Notification notification = new Notification
            {
                Name = ""+ name,
                Action = "Added",
                Date = DateTime.Now.ToString(),
                Did = Did
            };
            await AddNotification(notification);
        }

        public async Task EditEmployeeNotification(string name, int Did)
        {
            Notification notification = new Notification
            {
                Name = "" + name,
                Action = "Edit",
                Date = DateTime.Now.ToString(),
                Did = Did
            };
            await AddNotification(notification);
        }

        
    }
}
