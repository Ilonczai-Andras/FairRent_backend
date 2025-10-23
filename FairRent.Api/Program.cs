using FairRent.Application.Common.Interfaces;
using FairRent.Infrastructure.Persistence;
using FairRent.Api.Auth; // 🔹 fontos: Auth mappa importálása
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ===================================================
// 🔹 EF Core + PostgreSQL
// ===================================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"))
           .UseSnakeCaseNamingConvention());

builder.Services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

// ===================================================
// 🔹 Controllers + Swagger
// ===================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FairRent API",
        Version = "v1",
        Description = "FairRent REST API (ASP.NET Core + Auth0 JWT)",
        Contact = new OpenApiContact
        {
            Name = "FairRent",
            Email = "support@fairrent.local"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header. Példa: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ===================================================
// 🔹 Auth0 + Policy konfiguráció
// ===================================================
builder.Services.AddAuth0Jwt(builder.Configuration);
builder.Services.AddScopePolicies();

// ===================================================
// 🔹 Build & Middleware pipeline
// ===================================================
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "API running ✅");

app.Run();
