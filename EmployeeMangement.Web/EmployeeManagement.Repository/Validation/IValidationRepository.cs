using EmployeeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public interface IValidationRepository
    {
        Task<bool> CheckValidation(string email, string password);
        Task Logout();
    }
}
