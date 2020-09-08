using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public interface IPredefined
    {
        Task<bool> AddDefaulUser();
    }
}
