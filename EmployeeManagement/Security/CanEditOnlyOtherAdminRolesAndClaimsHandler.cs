using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagement.Security
{
    public class CanEditOnlyOtherAdminRolesAndClaimsHandler : 
        AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ManageAdminRolesAndClaimsRequirement requirement)
        {
            var authFileterContext = context.Resource as AuthorizationFilterContext;

            if (authFileterContext == null)
            {
                return Task.CompletedTask;
            }

            string logginInAdminId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string adminIdBeingEdited = authFileterContext.HttpContext.Request.Query["userId"];

            if (context.User.IsInRole("Admin")&&
                context.User.HasClaim(claim=>claim.Type=="Edit Role" && claim.Value=="true")&&
                adminIdBeingEdited.ToLower()!=logginInAdminId.ToLower())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
