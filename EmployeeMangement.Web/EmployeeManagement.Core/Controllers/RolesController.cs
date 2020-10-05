using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.ViewModel;
using EmployeeManagement.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace EmployeeManagement.Web.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IManager _manager;

        public RolesController(IManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IEnumerable<IdentityRole> GetRoles()
        {
            return _manager.GetAllRoles();
        }
        [HttpPost]
        public async Task<IActionResult> Add(IdentityRole identityRole)
        {
            identityRole.Name = identityRole.Name.Trim();
            if (identityRole.Id == "")
            {
                await _manager.AddRoleManager(identityRole);
            }
            else
            {
                identityRole.NormalizedName = identityRole.Name.ToUpper();
                await _manager.UpdateRoleManager(identityRole);

            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _manager.DeleteRoleManager(id);
            return NoContent();
        }
    }
}
