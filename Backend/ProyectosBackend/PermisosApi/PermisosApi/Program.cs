using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PermisosApi.Data;
using PermisosApi.Models;
using PermisosApi.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();

// Obtener clave secreta del archivo de configuración
var claveSecreta = builder.Configuration["Jwt:Key"];
var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));

//Configuracion JWT
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
    )
    };
});
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Tu API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",

        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT con el formato: Bearer {token}"
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

builder.Services.AddScoped<EmailService>();

var host = Environment.GetEnvironmentVariable("PGHOST") ?? "localhost";
var port = Environment.GetEnvironmentVariable("PGPORT") ?? "5432";
var db = Environment.GetEnvironmentVariable("PGDATABASE") ?? "railway";
var user = Environment.GetEnvironmentVariable("PGUSER") ?? "postgres";
var password = Environment.GetEnvironmentVariable("PGPASSWORD") ?? "miPasswordLocal";

var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true;";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(); 

app.MapControllers();

// Crear usuario admin automáticamente si no existe cuando no hay nada en la BD
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    //context.Database.Migrate();

    // Si no hay usuarios, crear el admin
    if (!context.Usuarios.Any())
    {
        var admin = new Usuario
        {
            Nombre = "Admin",
            Correo = "admin@admin.com",
            ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
            Rol = "Admin"
        };

        context.Usuarios.Add(admin);
        context.SaveChanges();
        Console.WriteLine("Usuario administrador creado automáticamente.");
    }
}

app.Run();
