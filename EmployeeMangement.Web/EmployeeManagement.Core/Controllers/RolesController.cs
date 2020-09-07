using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.ViewModel;
using EmployeeMangement.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IManager _manager;
        private readonly RolesViewModel _rolesViewModel;

        public RolesController(IManager manager)
        {
            _manager = manager;
            _rolesViewModel = new RolesViewModel();
        }

        public IActionResult Index()
        {
            _rolesViewModel.IdentityRoles = _manager.GetAllRoles();
            return View(_rolesViewModel);
        }

        public async Task<IActionResult> Add(IdentityRole identityRole)
        {
            identityRole.Name = identityRole.Name.Trim();
            if (identityRole.Id == null)
            {
                if (await _manager.AddRoleManager(identityRole))
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                identityRole.NormalizedName = identityRole.Name.ToUpper();
                if (await _manager.UpdateRoleManager(identityRole))
                {
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(string id)
        {
            if (await _manager.DeleteRoleManager(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
