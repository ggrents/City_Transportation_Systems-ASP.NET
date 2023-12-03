using City_Transportation_Systems.Data;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace City_Transportation_Systems.Repository
{
    public class BusRepository : IBusRepository
    {
        private CtsDbContext _db;
        public BusRepository(CtsDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateBusAsync(Bus bus)
        {
            
            await _db.AddAsync(bus);
            return await SaveChanges();
        }

        public async Task<bool> DeleteBusAsync(Bus bus)
        {
            _db.Remove(bus);
            return await SaveChanges();
        }

        public async Task<IEnumerable<Bus>> GetAllBusesAsync()
        {
            var buses =await _db.Buses.ToListAsync();
            return buses;
        }

        public async Task<Bus> GetBusByIdAsync(int id)
        {
            var bus = await _db.Buses.FirstOrDefaultAsync(b => b.Id == id) ;

            return bus;
        }

        public async Task<IEnumerable<Bus>> GetBusesByRouteAsync(int RouteId)
        {
            var buses = await _db.Buses.Where(b => b.RouteId == RouteId).ToListAsync();
            return buses;
        }

        public async Task<bool> UpdateBusAsync(Bus bus)
        {
            _db.Update(bus);
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
        {
            var isSaved = await _db.SaveChangesAsync();
            return isSaved > 0;
        }
    }
}
