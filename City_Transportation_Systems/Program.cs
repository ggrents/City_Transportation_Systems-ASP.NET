using Microsoft.EntityFrameworkCore;
using City_Transportation_Systems.Data;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Repository;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace City_Transportation_Systems
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => c.MapType<TimeSpan>(() => new OpenApiSchema { Type = "string", Example = new OpenApiString("00:00:00") }));  
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

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}