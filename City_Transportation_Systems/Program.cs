using Microsoft.EntityFrameworkCore;
using City_Transportation_Systems.Data;

namespace City_Transportation_Systems
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           // string connection = builder.Configuration.GetConnectionString("DefaultConnection");
           // builder.Services.AddDbContext<CtsDbContext>(options => options.UseSqlServer(connection));


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddDbContext<CtsDbContext>();
            builder.Services.AddDbContext<CtsDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
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