using GTU.Api.Data;
using GTU.Api.Extensions;
using GTU.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(op =>
//{
//    op.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Ingrese el token JWT en este formato: Bearer {token}"
//    });
//    // Requerir el esquema de seguridad por defecto
//    op.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "bearerAuth"
//                },
//                //Scheme = "Bearer",
//                //Name = "Bearer",
//                //In = ParameterLocation.Header
//            },
//            //new List<string>()
//            new string[] { }
//        }
//    });
//}
//);

builder.Services.AddSwaggerConfiguration();

// Cors para el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // origen del frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

//BD
var connnectionString = builder.Configuration.GetConnectionString("TodoContext");
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlite(connnectionString));

// Seguridad
builder.Services.AddJwtAuthentication(builder.Configuration);


//var key = builder.Configuration["Jwt:Key"];
//var keyBytes = Encoding.UTF8.GetBytes(key);
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            // se dejan validaciones simples para el ejemplo
//            ValidateIssuer = false,
//            ValidateAudience = false,
//            ValidateLifetime = false,
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
//        };
//    });


//builder.Services.AddAuthorization();


//inyecciones
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();


var app = builder.Build();

// seguridad
app.UseAuthentication();
app.UseAuthorization();


//Cors
app.UseCors("AllowAngularApp");

//swagger
app.useSwaggerConfiguration();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

