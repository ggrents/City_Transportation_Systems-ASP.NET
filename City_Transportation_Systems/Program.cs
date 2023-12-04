using Microsoft.EntityFrameworkCore;
using City_Transportation_Systems.Data;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Repository;

namespace City_Transportation_Systems
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();  
            builder.Services.AddDbContext<CtsDbContext>();
            builder.Services.AddScoped<IBusRepository, BusRepository>();      
            builder.Services.AddScoped<IRouteRepository, RouteRepository>();      
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