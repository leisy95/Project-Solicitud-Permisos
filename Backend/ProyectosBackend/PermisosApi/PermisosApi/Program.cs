//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using PermisosApi.Data;
//using PermisosApi.Models;
//using PermisosApi.Services;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp",
//        policy => policy.WithOrigins("http://localhost:4200")
//                        .AllowAnyHeader()
//                        .AllowAnyMethod());
//});

//// Add services to the container.

//builder.Services.AddControllers();

//// Obtener clave secreta del archivo de configuración
//var claveSecreta = builder.Configuration["Jwt:Key"];
//var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));

////Configuracion JWT
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(
//        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
//    )
//    };
//});
//builder.Services.AddAuthorization();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new() { Title = "Tu API", Version = "v1" });

//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",

//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Ingrese el token JWT con el formato: Bearer {token}"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            Array.Empty<string>()
//        }
//    });
//});

//builder.Services.AddScoped<EmailService>();

//var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
//                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(connectionString));


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseCors("AllowAngularApp");

//app.UseAuthentication();

//app.UseAuthorization();

//app.UseStaticFiles(); 

//app.MapControllers();

//// Crear usuario admin automáticamente si no existe cuando no hay nada en la BD
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var context = services.GetRequiredService<ApplicationDbContext>();

//    //context.Database.Migrate();

//    // Si no hay usuarios, crear el admin
//    if (!context.Usuarios.Any())
//    {
//        var admin = new Usuario
//        {
//            Nombre = "Admin",
//            Correo = "admin@admin.com",
//            ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
//            Rol = "Admin"
//        };

//        context.Usuarios.Add(admin);
//        context.SaveChanges();
//        Console.WriteLine("Usuario administrador creado automáticamente.");
//    }
//}

//app.Run();

//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using PermisosApi.Data;
//using PermisosApi.Models;
//using PermisosApi.Services;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// CORS para Angular
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp",
//        policy => policy.WithOrigins("http://localhost:4200")
//                        .AllowAnyHeader()
//                        .AllowAnyMethod());
//});

//// Controladores
//builder.Services.AddControllers();

//// JWT
//var claveSecreta = builder.Configuration["Jwt:Key"];
//var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = clave
//    };
//});

//builder.Services.AddAuthorization();

//// Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new() { Title = "Tu API", Version = "v1" });

//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Ingrese el token JWT con el formato: Bearer {token}"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            Array.Empty<string>()
//        }
//    });
//});

//// Servicio de correo
//builder.Services.AddScoped<EmailService>();

//// Conexión a la base de datos
//var connectionStringEnv = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
//string connectionString;

//if (!string.IsNullOrEmpty(connectionStringEnv))
//{
//    // Si es URI de PostgreSQL tipo Railway
//    if (connectionStringEnv.StartsWith("postgres://") || connectionStringEnv.StartsWith("postgresql://"))
//    {
//        var uri = new Uri(connectionStringEnv);
//        var userInfo = uri.UserInfo.Split(':');

//        connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};Pooling=true;SSL Mode=Require;Trust Server Certificate=true;";
//    }
//    else
//    {
//        // Si ya es cadena en formato ADO.NET (opcional)
//        connectionString = connectionStringEnv;
//    }
//}
//else
//{
//    // Fallback a appsettings.json (entorno local)
//    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//}

//// Debug: mostrar cadena final
//Console.WriteLine($"Conexión a la DB: {connectionString}");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(connectionString));

//var app = builder.Build();

//// Swagger en desarrollo
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseCors("AllowAngularApp");
//app.UseAuthentication();
//app.UseAuthorization();
//app.UseStaticFiles();
//app.MapControllers();

//// Aplicar migraciones y crear admin automáticamente
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var context = services.GetRequiredService<ApplicationDbContext>();

//    try
//    {
//        // Migraciones automáticas
//        context.Database.Migrate();
//        Console.WriteLine("Migraciones aplicadas correctamente.");

//        // Crear admin si no hay usuarios
//        if (!context.Usuarios.Any())
//        {
//            var admin = new Usuario
//            {
//                Nombre = "Admin",
//                Correo = "admin@admin.com",
//                ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
//                Rol = "Admin"
//            };

//            context.Usuarios.Add(admin);
//            context.SaveChanges();
//            Console.WriteLine("Usuario administrador creado automáticamente.");
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("Error al inicializar la base de datos: " + ex);
//        throw; // Opcional: lanzar la excepción para que Render lo registre
//    }
//}

//app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PermisosApi.Data;
using PermisosApi.Models;
using PermisosApi.Services;
using System;
using System.Text;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// ------------------- CORS -------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("https://project-solicitud-permisos.vercel.app")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// ------------------- Controllers -------------------
builder.Services.AddControllers();

// ------------------- JWT -------------------
var claveSecreta = builder.Configuration["Jwt:Key"];
var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));

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
        IssuerSigningKey = clave
    };
});

builder.Services.AddAuthorization();

// ------------------- Swagger -------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Permisos API", Version = "v1" });

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

// ------------------- Servicios -------------------
builder.Services.AddScoped<EmailService>();

// ------------------- Configuración de DB -------------------

// Leer DATABASE_PUBLIC_URL de Railway
var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL")
                 ?? Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL");

if (string.IsNullOrEmpty(connectionUrl))
{
    throw new Exception("No se encontró la cadena de conexión de la base de datos en las variables de entorno.");
}

// Convertir URL de Railway a formato que Npgsql entienda
var databaseUri = new Uri(connectionUrl);
var userInfo = databaseUri.UserInfo.Split(':');

var builderConn = new Npgsql.NpgsqlConnectionStringBuilder
{
    Host = databaseUri.Host,
    Port = databaseUri.Port,
    Username = userInfo[0],
    Password = userInfo[1],
    Database = databaseUri.LocalPath.TrimStart('/'),
    SslMode = SslMode.Prefer,
    TrustServerCertificate = true
};

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builderConn.ConnectionString));

var app = builder.Build();

//// ------------------- Middleware -------------------
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapMethods("{*path}", new[] { "OPTIONS" }, () => Results.Ok())
   .WithMetadata(new Microsoft.AspNetCore.Cors.EnableCorsAttribute("AllowAngularApp"));

app.UseStaticFiles();

app.MapControllers();

app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "https://project-solicitud-permisos.vercel.app");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
        context.Response.StatusCode = 204;
        return;
    }

    await next();
});

// ------------------- Inicializar DB y crear admin -------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    try
    {
        // Solo correr migraciones si estamos en producción
        if (app.Environment.IsProduction())
        {
            Console.WriteLine("Iniciando migraciones en producción...");
            context.Database.Migrate();
            Console.WriteLine("Migraciones completadas.");

            // Crear usuario admin si no existe
            if (!context.Usuarios.Any(u => u.Correo == "admin@admin.com"))
            {
                var admin = new Usuario
                {
                    Nombre = "Admin",
                    Correo = "admin@admin.com",
                    ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("1234"),
                    Rol = "Admin"
                };

                context.Usuarios.Add(admin);
                context.SaveChanges();
                Console.WriteLine("Usuario administrador creado automáticamente.");
            }
            else
            {
                Console.WriteLine("El usuario administrador ya existe en la base de datos.");
            }
        }
        else
        {
            Console.WriteLine("Entorno local detectado: no se ejecutan migraciones.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al inicializar la base de datos: " + ex.Message);
        if (ex.InnerException != null)
            Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
        throw;
    }
}
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");