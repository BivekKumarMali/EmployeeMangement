﻿using EmployeeManagement.Web.Models;
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

        public NotificationRepository(AppDbContext context, IHubContext<SignalServer> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        

        public IEnumerable<Notification> GetNotifications(string userId)
        {
            var notifications = _context.Notifications.ToList();
            var reads = _context.IsReads.ToList();

            foreach (var read in reads)
            {
                if(read.UserId == userId)
                {
                    Notification notification = _context.Notifications.Find(read.Nid);
                    notifications.Remove(notification);
                }
            }
            return notifications;
        }

        public IEnumerable<Notification> GetNotificationsByDid(int Did,string userId)
        {

            var notifications = _context.Notifications.ToList();
            var reads = _context.IsReads.ToList();

            foreach (var read in reads)
            {
                if (read.UserId == userId)
                {
                    Notification notification = _context.Notifications.Find(read.Nid);
                    notifications.Remove(notification);
                }
            }
            return notifications.Where(x => x.Did == Did);
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
             AddNotification(notification);
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
             AddNotification(notification);
        }

        public async Task AddEmployeeNotification(Employee employee)
        {
            Notification notification = new Notification
            {
                Name = employee.Name +" "+ employee.Surname,
                Action = "Added",
                Date = DateTime.Now.ToString(),
                Did = employee.Did
            };
             AddNotification(notification);
        }

        public async Task EditEmployeeNotification(Employee employee)
        {
            Notification notification = new Notification
            {
                Name = employee.Name + " " + employee.Surname,
                Action = "Edit",
                Date = DateTime.Now.ToString(),
                Did = employee.Did
            };
             AddNotification(notification);
        }

        public void DeleteDepartmentNotification()
        {
            Notification notification = new Notification
            {
                Name = "Department",
                Action = "Delete",
                Date = DateTime.Now.ToString(),
                Did = 0
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
                Nid = nid,
                UserId = userId
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