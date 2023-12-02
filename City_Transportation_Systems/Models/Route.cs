namespace City_Transportation_Systems.Models
{
    public class Route
    {
        public int Id { get; set; }
        public int Number { get; set; }

        ICollection<Bus> Buses { get; set;}
        public ICollection<Schedule> Schedules { get; set; }
    }
}
