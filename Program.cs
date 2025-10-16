using GTU.Api.Data;
using GTU.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

//inyecciones
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenService>();


builder.Services.AddControllers();
var app = builder.Build();


//Cors
app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();



app.Run();

