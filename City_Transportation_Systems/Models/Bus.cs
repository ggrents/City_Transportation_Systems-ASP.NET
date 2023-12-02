namespace City_Transportation_Systems.Models
{
    public class Bus
    {
        public int Id { get; set; }

        public string? Model { get; set; }

        public int Capacity { get; set; }

        public int RouteId { get; set; }
        
        public Route? Route { get; set; }

       
    }
}
