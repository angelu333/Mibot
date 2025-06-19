// Mibot/Program.cs

using Mibot.Services.Features.Mangas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// AQUÍ REGISTRAMOS NUESTRO SERVICIO
// Usaremos Singleton para que la lista en memoria persista durante la vida de la aplicación.
builder.Services.AddScoped<IMangaRepository, MangaRepository>();
builder.Services.AddScoped<MangaService>();

// Configuración de autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "TU_ISSUER", // <-- Cambia esto por tu issuer
        ValidAudience = "TU_AUDIENCE", // <-- Cambia esto por tu audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TU_CLAVE_SECRETA")) // <-- Cambia esto por tu clave secreta
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MangaBot API",
        Version = "v1",
        Description = "API para gestionar una colección de mangas",
        Contact = new OpenApiContact
        {
            Name = "Tu Equipo",
            Email = "tu@email.com"
        }
    });
    // Configuración para JWT Bearer en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT en este formato: Bearer {tu token}"
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
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MangaBot API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization(); // Lo veremos más adelante
app.MapControllers();   // Para que las peticiones lleguen a nuestros Controllers

app.Run();