using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Filter
{
    public class EmployeeNumberOfYears:IAuthorizationRequirement
    {
        public EmployeeNumberOfYears(int Age)
        {
            AGE = Age;
        }
        public int AGE { get; set; }
    }
}
