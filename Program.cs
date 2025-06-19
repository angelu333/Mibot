// Mibot/Program.cs

using Mibot.Services.Features.Mangas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// AQUÍ REGISTRAMOS NUESTRO SERVICIO
// Usaremos Singleton para que la lista en memoria persista durante la vida de la aplicación.
builder.Services.AddScoped<IMangaRepository, MangaRepository>();
builder.Services.AddScoped<MangaService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "MangaBot API",
        Version = "v1",
        Description = "API para gestionar una colección de mangas",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Tu Equipo",
            Email = "tu@email.com"
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
app.UseAuthorization(); // Lo veremos más adelante
app.MapControllers();   // Para que las peticiones lleguen a nuestros Controllers

app.Run();