using EasyLife.Domain.GlobalConstants;
using EasyLife.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EasyLife.Application.Services.MiddlewareServices
{
    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate _next;
        private RoleManager<IdentityRole> roleManager;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;

            await CheckOrAddRoles();

            await _next(context);
        }
        public async Task CheckOrAddRoles()
        {
            var memberRole = await roleManager.RoleExistsAsync(RoleType.Member);
            var adminRole = await roleManager.RoleExistsAsync(RoleType.Administrator);
            var moderatorRole = await roleManager.RoleExistsAsync(RoleType.Moderator);

            if (!memberRole)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = RoleType.Member });
                var memberRoleName = await roleManager.FindByNameAsync(RoleType.Member);
                await roleManager.AddClaimAsync(memberRoleName, new Claim(ClaimTypes.Role, RoleType.Member));
            }

            if (!adminRole)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = RoleType.Administrator });
                var adminRoleName = await roleManager.FindByNameAsync(RoleType.Administrator);
                await roleManager.AddClaimAsync(adminRoleName, new Claim(ClaimTypes.Role, RoleType.Administrator));
            }

            if (!moderatorRole)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = RoleType.Moderator });
                var moderatorRoleName = await roleManager.FindByNameAsync(RoleType.Moderator);
                await roleManager.AddClaimAsync(moderatorRoleName, new Claim(ClaimTypes.Role, RoleType.Moderator));
            }
        }
    }
}
