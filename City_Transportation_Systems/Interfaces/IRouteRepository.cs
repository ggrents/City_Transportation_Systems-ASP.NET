using Route = City_Transportation_Systems.Models.Route;

namespace City_Transportation_Systems.Interfaces
{
    public interface IRouteRepository
    {
        Task<IEnumerable<Route>> GetAllRoutesAsync();
        Task<Route> GetRouteByIdAsync(int id);
        Task<IEnumerable<Route>> GetRoutesByStation(int StationId);
        Task<bool> CreateRouteAsync(Route route);
        Task<bool> UpdateRouteAsync(Route route);
        Task<bool> DeleteRouteAsync(Route route);

    }
}
