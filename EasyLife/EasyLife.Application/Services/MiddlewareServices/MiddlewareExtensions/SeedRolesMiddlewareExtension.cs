using Microsoft.AspNetCore.Builder;

namespace EasyLife.Application.Services.MiddlewareServices.MiddlewareExtensions
{
    public static class SeedRolesMiddlewareExtension
    {
        public static IApplicationBuilder UseSeedRoles(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedRolesMiddleware>();
        }
    }
}
