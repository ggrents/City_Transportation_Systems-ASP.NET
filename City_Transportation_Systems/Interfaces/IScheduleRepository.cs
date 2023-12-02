using City_Transportation_Systems.Models;

namespace City_Transportation_Systems.Interfaces
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetFullScheduleAsync();
        Task<Schedule> GetScheduleByIdAsync(int id);
        Task<IEnumerable<Schedule>> GetSchedulesByRoute(int RouteId);
        Task<IEnumerable<Schedule>> GetSchedulesByStation(int StationId);
        Task<IEnumerable<Schedule>> GetSchedulesByTimeAndStation(TimeSpan time , int StationId);
        Task<IEnumerable<Schedule>> GetSchedulesByTimeAndRoute(TimeSpan time , int RouteId);
        Task<bool> CreateScheduleAsync(Schedule schedule);
        Task<bool> UpdateScheduleAsync(Schedule schedule);
        Task<bool> DeleteScheduleAsync(Schedule schedule);

    }
}
