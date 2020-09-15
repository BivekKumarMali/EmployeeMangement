using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repository
{
    public class ValidationRepository : IValidationRepository
    {
        private SignInManager<IdentityUser> _signInManager;

        public ValidationRepository(SignInManager<IdentityUser> signInManager)
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
