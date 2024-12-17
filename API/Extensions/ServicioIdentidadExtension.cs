using Data.Interfaces;
using Data.Servicios;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Models.Entidades;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions
{
    public static class ServicioIdentidadExtension
    {
        public static IServiceCollection AgregarServiciosIdentidad(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<UserAplication>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<RoleAplication>()
                .AddRoleManager<RoleManager<RoleAplication>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuerSigningKey = true,
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                             ValidateIssuer = false,
                             ValidateAudience = false
                         };
                    });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminRol", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("AllRol", policy => policy.RequireRole("Admin", "User", "Employee"));
                opt.AddPolicy("UserEmplyeddRol", policy => policy.RequireRole("User", "Employee"));
            });

            return services;
        }
    }
}
