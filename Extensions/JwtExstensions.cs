using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GTU.Api.Extensions
{
    public static class JwtExstensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration conf)
        {
            var key = conf["Jwt:Key"];
            var keyBytes = Encoding.UTF8.GetBytes(key);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // se dejan validaciones simples para el ejemplo
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                    };
                });


            services.AddAuthorization();
            return services;
        }
    }
}
