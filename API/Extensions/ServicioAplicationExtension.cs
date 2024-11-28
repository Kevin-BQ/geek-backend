using Data.Interfaces;
using Data.Servicios;
using Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using Data.Interfaces.IRepositorio;
using Data.Repositorio;
using Utils;
using BLL.Services.Interfaces;
using BLL.Services;

namespace API.Extensions
{
    public static class ServicioAplicationExtension
    {
        public static IServiceCollection AgregarServiciosAplicacion(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Ingresa Bearer [espacio] token \r\n\r\n " +
                                  "Ejemplo: Bearer ejoy^88788999990000",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                 });
            });

            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                  .Where(e => e.Value.Errors.Count > 0)
                                  // .SelectMany(x => x)
                                  .Aggregate(new Dictionary<string, string>(), (dict, entry) =>
                                  {
                                      var attributeName = ApiValidationErrorResponse.AttributeToCamelCase(entry.Key);
                                      dict[attributeName] = entry.Value.Errors[0].ErrorMessage;
                                      return dict;
                                  });
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors,
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddScoped<IWorkUnit, WorkUnit>();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUploadImage, UploadImage>();
            services.AddScoped<IBrandService , BrandService>();


            return services;
        }
    }
}
