using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.OpenApi.Models;

namespace GTU.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services) {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(op =>
            {
                op.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ingrese el token JWT en este formato: Bearer {token}"
                });
                // Requerir el esquema de seguridad por defecto
                op.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            },
                            //Scheme = "Bearer",
                            //Name = "Bearer",
                            //In = ParameterLocation.Header
                        },
                        //new List<string>()
                        new string[] { }
                    }
                });
            });

            return services;
        }

        public static WebApplication useSwaggerConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(op => {
                    op.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    //op.RoutePrefix = string.Empty;
                });
            }
            return app;
        }
    }
}
