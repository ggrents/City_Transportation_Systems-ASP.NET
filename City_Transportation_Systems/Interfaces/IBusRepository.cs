using City_Transportation_Systems.Models;
namespace City_Transportation_Systems.Interfaces
{
    public interface IBusRepository
    {
        Task<IEnumerable<Bus>> GetAllBusesAsync();
        Task<Bus> GetBusByIdAsync(int id);
        Task<IEnumerable<Bus>> GetBusesByRouteAsync(int RouteId);
        Task<bool> CreateBusAsync(Bus bus);
        Task<bool> UpdateBusAsync(Bus bus);
        Task<bool> DeleteBusAsync(Bus bus);

    }
}
