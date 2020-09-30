using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.ViewModel;
using EmployeeManagement.Web.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeManagement.Web.EmployeeManagement.Core.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : Controller
    {
        
        private readonly IValidationRepository _validation;
        private readonly IManager _manager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPredefined _predefined; 
        private UserManager<IdentityUser> _userManager;

        public AccountController(IValidationRepository validation, IManager manager, IEmployeeRepository employeeRepository, IPredefined predefined, UserManager<IdentityUser> userManager)
        {
            _validation = validation;
            _manager = manager;
            _employeeRepository = employeeRepository;
            _predefined = predefined;
            _userManager = userManager;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LogInViewModel logInViewModel)
        {
            if (!await _predefined.AddDefaulAdminUser() && !await _predefined.AddDefaulHRUser())
            {
                if (ModelState.IsValid && logInViewModel.Email != null)
                {
                    if (await _validation.CheckValidation(logInViewModel.Email, logInViewModel.Password))
                    {
                        var user = await _manager.GetUserByEmail(logInViewModel.Email);
                        var employee = _employeeRepository.GetEmployeeByUserId(user.Id);
                        var role = await _userManager.GetRolesAsync(user);

                        IdentityOptions _options = new IdentityOptions();

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim("Did",employee.Did.ToString()),
                        new Claim("Name",employee.Name.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                    }),
                            Expires = DateTime.UtcNow.AddMinutes(1),
                        };
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                        var token = tokenHandler.WriteToken(securityToken);
                        return Ok(new { token });
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
            }
            return BadRequest(new { message = "Username or password is incorrect." });

        }

        public IActionResult Logout()
        {
            _validation.Logout();
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ForgetPassword(PasswordResetView passwordResetView)
        {
            if(passwordResetView.Email != null) 
            {
                if( await _manager.UpdatePassword(passwordResetView))
                {
                    return Ok();
                }
            }

            return BadRequest(new { message = "Username or password is incorrect." });
        }

    }
}
