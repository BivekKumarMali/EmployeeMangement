using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repository
{
    public class NotficationRepository : INotificationRepository
    {
        public AppDbContext _context { get; set; }
        public NotficationRepository()
        {

        }
        public void AddNotification(Notification notification)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Notification>> GetNotifications()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Notification>> GetNotificationsByDid()
        {
            throw new NotImplementedException();
        }
    }
}
