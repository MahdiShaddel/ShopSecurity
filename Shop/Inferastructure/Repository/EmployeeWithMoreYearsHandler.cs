using Microsoft.AspNetCore.Authorization;
using Shop.Filter;
using Shop.Inferastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Inferastructure.Repository
{
    public class EmployeeWithMoreYearsHandler : AuthorizationHandler<EmployeeNumberOfYears>
    {
        private readonly IEmployeeNumberOfYears employeeNumberOfYears;
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            EmployeeNumberOfYears requirement)
        {
            if (!context.User.HasClaim(c=>c.Type==ClaimTypes.Name))
            {
                return Task.CompletedTask;
            }
            var name = context.User.FindFirst(c => c.Type == ClaimTypes.Name);
            var YearsOfExpreience = employeeNumberOfYears.Get(name.Value);

            if (YearsOfExpreience>=requirement.AGE)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
