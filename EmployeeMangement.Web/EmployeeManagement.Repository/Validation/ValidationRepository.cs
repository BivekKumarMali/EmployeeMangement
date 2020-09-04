using EmployeeManagement.Web.Models;
using EmployeeMangement.Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public class ValidationRepository : IValidationRepository
    {
        private SignInManager<Roles> _signInManager;

        public ValidationRepository(SignInManager<Roles> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> CheckValidation(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, true, false);
            return result.Succeeded;
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
