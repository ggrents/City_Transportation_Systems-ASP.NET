using Microsoft.EntityFrameworkCore;
using City_Transportation_Systems.Data;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Repository;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Reflection;
using City_Transportation_Systems.Middleware;

namespace City_Transportation_Systems
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
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
            new string[] { }
        }
    });


                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "City Transportation Systems API",
                    Version = "v1",
                    Description = "An API, designed to simplify the use of public transport by citizens",
                    Contact = new OpenApiContact
                    {
                        Name = "Artem Grents",
                        Email = "grents-04@mail.ru",
                        Url = new Uri("https://t.me/gggrents"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "CTS API LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                c.MapType<TimeSpan>(() => new OpenApiSchema { Type = "string", Example = new OpenApiString("00:00:00") });
                 var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            }
                
                );  
            builder.Services.AddDbContext<CtsDbContext>();
            builder.Services.AddScoped<IBusRepository, BusRepository>();      
            builder.Services.AddScoped<IRouteRepository, RouteRepository>();      
            builder.Services.AddScoped<IDriverRepository, DriverRepository>();      
            builder.Services.AddScoped<IStationRepository, StationRepository>();      
            builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();      
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

           
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<TokenMiddleware>();
            app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}