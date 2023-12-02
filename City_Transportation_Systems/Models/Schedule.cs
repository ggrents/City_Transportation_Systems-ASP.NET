using System.ComponentModel.DataAnnotations.Schema;

namespace City_Transportation_Systems.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan TimeStamp { get; set; }

        public int StationId { get; set; }
        public Station? Station { get; set; }

        public int RouteId { get; set; }
        public Route? Route { get; set; }
    }
}
