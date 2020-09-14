using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repository
{
    public interface IPredefined
    {
        Task<bool> AddDefaulAdminUser();
        Task<bool> AddDefaulHRUser();
    }
}
