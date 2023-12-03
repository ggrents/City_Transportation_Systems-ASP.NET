using City_Transportation_Systems.Data;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Transportation_Systems.Repository
{
    public class StationRepository : IStationRepository
    {

        private CtsDbContext _db;
        public StationRepository(CtsDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateStationAsync(Station station)
        {
            await _db.AddAsync(station);
            return await SaveChanges();
        }

        public async Task<bool> DeleteStationAsync(Station station)
        {
            _db.Remove(station);
            return await SaveChanges();
        }

        public async Task<IEnumerable<Station>> GetAllStationsAsync()
        {
            var stations = await _db.Stations.ToListAsync();
            return stations;
        }

        public async Task<Station> GetStationByIdAsync(int id)
        {
            var station = await _db.Stations.FirstOrDefaultAsync(r => r.Id == id);
            return station;
        }

        public async Task<IEnumerable<Station>> GetStationsByRoute(int RouteId)
        {
            var stations = await _db.Stations
            .Where(station => station.Schedules.Any(schedule => schedule.RouteId == RouteId))
            .ToListAsync();
            return stations;
        }

        public async Task<bool> UpdateStationAsync(Station station)
        {
            _db.Update(station);
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
        {
            var isSaved = await _db.SaveChangesAsync();
            return isSaved > 0;
        }

    }
}
