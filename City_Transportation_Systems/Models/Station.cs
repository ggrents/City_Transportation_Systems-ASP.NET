namespace City_Transportation_Systems.Models
{
    public class Station
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}
