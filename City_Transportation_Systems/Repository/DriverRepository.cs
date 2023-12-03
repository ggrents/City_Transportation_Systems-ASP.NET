using City_Transportation_Systems.Data;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Transportation_Systems.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private CtsDbContext _db;
        public DriverRepository(CtsDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateDriverAsync(Driver driver)
        { 
            await _db.AddAsync(driver);
            return await SaveChanges();
        }

        public async Task<bool> DeleteDriverAsync(Driver driver)
        {

            _db.Remove(driver);
            return await SaveChanges();
        }

        public async Task<IEnumerable<Driver>> GetAllDriversAsync()
        {
           var drivers = await _db.Drivers.ToListAsync();
            return drivers;
        }

        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            var driver = await _db.Drivers.FirstOrDefaultAsync(b=>b.Id == id);
            return driver;
        }

        public async Task<bool> UpdateDriverAsync(Driver driver)
        {
            _db.Update(driver);
            return await SaveChanges();
        }


        private async Task<bool> SaveChanges()
        {
            var isSaved = await _db.SaveChangesAsync();
            return isSaved > 0;
        }
    }
}
