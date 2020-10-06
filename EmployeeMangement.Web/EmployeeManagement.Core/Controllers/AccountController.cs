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
using System.Text;
using Microsoft.Extensions.Configuration;

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
        private readonly IConfiguration _configuration;

        public AccountController(
            IValidationRepository validation,
            IManager manager,
            IEmployeeRepository employeeRepository,
            IPredefined predefined,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration
            )
        {
            _validation = validation;
            _manager = manager;
            _employeeRepository = employeeRepository;
            _predefined = predefined;
            _userManager = userManager;
            _configuration = configuration;
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
                        var authClaims = new List<Claim>
                        {
                            new Claim("name", user.UserName),
                            new Claim("Did", employee.Did.ToString()),
                            new Claim("UserID", employee.UserId.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };

                        foreach (var userRole in role)
                        {
                            authClaims.Add(new Claim("role", userRole));
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        }
                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                        var token = new JwtSecurityToken(
                            issuer: _configuration["JWT:ValidIssuer"],
                            audience: _configuration["JWT:ValidAudience"],
                            expires: DateTime.Now.AddHours(3),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                    return Unauthorized();
                }
            }

            return Unauthorized();

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
