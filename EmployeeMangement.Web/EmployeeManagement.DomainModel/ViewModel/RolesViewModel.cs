using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.ViewModel
{
    public class RolesViewModel
    {
        public IdentityRole identityRole { get; set; }
        public List<IdentityRole> IdentityRoles { get; set; }
    }
}
