
using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace MicroService_NaceTuIdea
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationServices(builder.Configuration);

            // Configura el DbContext utilizando la cadena de conexi�n de la configuraci�n
            builder.Services.AddDbContext<AppDbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

            builder.Services.AddControllers();

            // Configuraci�n de Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "NaceTuIdea", Version = "v1" });
            });

            // Configuraci�n de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .AllowAnyOrigin()    // Permitir cualquier origen
                        .AllowAnyMethod()    // Permitir cualquier m�todo HTTP
                        .AllowAnyHeader());  // Permitir cualquier cabecera
            });

            // Configuraci�n de JWT
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
                    ValidIssuer = builder.Configuration["Jwt:Issuer"], // El emisor del token
                    ValidAudience = builder.Configuration["Jwt:Audience"], // La audiencia del token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"])) // Clave para firmar el token
                };
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigin");

            app.UseRouting(); 

            app.UseAuthentication(); // Agregar autenticaci�n antes de la autorizaci�n
            app.UseAuthorization(); // Se mantiene, ahora con configuraci�n JWT

            app.MapControllers();

            app.Run();
        }
    }

}
