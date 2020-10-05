using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Repository;
using EmployeeManagement.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeManagement.Web.EmployeeManagement.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<SignalServer> _hub;
        private INotificationRepository _notificationRepository;
        private readonly AppDbContext _context;

        public NotificationController(
            IHubContext<SignalServer> hub,
            INotificationRepository notificationRepository,
            AppDbContext context
            )
        {
            _hub = hub;
            _notificationRepository = notificationRepository;
            _context = context;
        }
        [HttpGet]
        [Route("GetNotification")]
        public IActionResult Get()
        {
            var result = Notification();
            _hub.Clients.All.SendAsync("transferchartdata", result.Result);
            return Ok(new { result.Result});
        }
        [HttpPost]
        public async Task<IEnumerable<Notification>> Notification()
        {
                return _notificationRepository.GetNotifications();
        }

        [HttpPost("{nid_userId}")]
        public void ReadNotification(string nid_userId)
        {
            var newnid_userId = nid_userId.Split(" ");
            _notificationRepository.IsReadNotification(int.Parse(newnid_userId[0]), newnid_userId[1]);
        }

        [HttpGet("{userID}")]
        [Route("GetIsRead/{userID}")]
        public IEnumerable<IsRead> GetIsRead(string userID)
        {
            return _context.IsReads.ToList().Where(x => x.userId == userID);
        }
    }
}
