using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Inferastructure.Repository
{
    public class CustomRequireClaim : IAuthorizationRequirement
    {
        public string ClaimType { get; set; }
        public CustomRequireClaim(string claimType)
        {
            ClaimType = claimType;
        }
    }
}
