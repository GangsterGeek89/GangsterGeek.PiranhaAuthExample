using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Piranha;
using Piranha.AspNetCore;
using GangsterGeek.Piranha.AuthSystem;
using Microsoft.Extensions.Configuration;

public static class AuthSystemExtensions
{
    /// <summary>
    /// Adds the GangsterGeek.Piranha.AuthSystem module.
    /// </summary>
    /// <param name="serviceBuilder"></param>
    /// <returns></returns>
    public static PiranhaServiceBuilder UseGangsterGeekAuthSystem(this PiranhaServiceBuilder serviceBuilder, IConfiguration configuration)
    {
        serviceBuilder.Services.AddAuthentication().AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration["Google:ClientId"];
            googleOptions.ClientSecret = configuration["Google:ClientSecret"];
        });

        

        return serviceBuilder;
    }

    /// <summary>
    /// Uses the GangsterGeek.Piranha.AuthSystem module.
    /// </summary>
    /// <param name="applicationBuilder">The current application builder</param>
    /// <returns>The builder</returns>
    public static PiranhaApplicationBuilder UseGangsterGeekAuthSystem(this PiranhaApplicationBuilder applicationBuilder)
    {
        

        return applicationBuilder;
    }

    /// <summary>
    /// Static accessor to GangsterGeek.Piranha.AuthSystem module if it is registered in the Piranha application.
    /// </summary>
    /// <param name="modules">The available modules</param>
    /// <returns>The GangsterGeek.Piranha.AuthSystem module</returns>
    public static AuthModule GangsterGeekPiranhaAuthSystem(this Piranha.Runtime.AppModuleList modules)
    {
        return modules.Get<AuthModule>();
    }
}
