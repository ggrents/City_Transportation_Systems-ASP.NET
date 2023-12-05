using City_Transportation_Systems.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Route = City_Transportation_Systems.Models.Route;

namespace City_Transportation_Systems.DTO
{
    public class ScheduleDTO
    {
        public int Id { get; set; }

        public TimeSpan TimeStamp { get; set; }

        public int StationId { get; set; }
        public Station? Station { get; set; }

        public int RouteId { get; set; }
        
        public Route? Route { get; set; }
    }

    public class CreateScheduleDTO
    {
        [Range(typeof(TimeSpan), "00:00:00", "23:59:59", ErrorMessage = "The field {0} must be between {1} and {2}.")]
        public TimeSpan TimeStamp { get; set; }

        public int StationId { get; set; }

        public int RouteId { get; set; }
    }
}
