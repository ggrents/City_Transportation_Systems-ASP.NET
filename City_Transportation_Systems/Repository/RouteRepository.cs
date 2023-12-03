using City_Transportation_Systems.Data;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Models;
using Microsoft.EntityFrameworkCore;
using Route = City_Transportation_Systems.Models.Route;

namespace City_Transportation_Systems.Repository
{
    public class RouteRepository : IRouteRepository
    {

        private CtsDbContext _db;
        public RouteRepository(CtsDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateRouteAsync(Route route)
        {
            await _db.AddAsync(route);
            return await SaveChanges();
        }

        public async Task<bool> DeleteRouteAsync(Route route)
        {
            _db.Remove(route);
            return await SaveChanges();
        }

        public async Task<IEnumerable<Route>> GetAllRoutesAsync()
        {
            var routes = await _db.Routes.ToListAsync();
            return routes;
        }

        public async Task<Route> GetRouteByIdAsync(int id)
        {
            var route = await _db.Routes.FirstOrDefaultAsync(r=>r.Id==id);
            return route;
        }

        public async Task<IEnumerable<Route>> GetRoutesByStation(int StationId)
        {
            var routes = await _db.Routes
            .Where(route => route.Schedules.Any(schedule => schedule.StationId == StationId))
            .ToListAsync();
            return routes;
        }

        public async Task<bool> UpdateRouteAsync(Route route)
        {
            _db.Update(route);
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
        {
            var isSaved = await _db.SaveChangesAsync();
            return isSaved > 0;
        }
    }
}
