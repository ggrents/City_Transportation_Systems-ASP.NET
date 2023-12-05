using System.Text.Json.Serialization;

namespace City_Transportation_Systems.Models
{
    public class Route
    {
        public int Id { get; set; }
        public int Number { get; set; }

        ICollection<Bus> Buses { get; set;}

        [JsonIgnore]
        public ICollection<Schedule> Schedules { get; set; }
    }
}
