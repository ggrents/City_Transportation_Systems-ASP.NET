using City_Transportation_Systems.Models;

namespace City_Transportation_Systems.Interfaces
{
    public interface IStationRepository
    {
        Task<IEnumerable<Station>> GetAllStationsAsync();
        Task<Station> GetStationByIdAsync(int id);
        Task<IEnumerable<Station>> GetStationsByRoute(int RouteId);
        Task<bool> CreateStationAsync(Station station);
        Task<bool> UpdateStationAsync(Station station);
        Task<bool> DeleteStationAsync(Station station);
    }
}
