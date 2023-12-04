namespace City_Transportation_Systems.DTO
{
    public class BusDTO
    {
        public int Id { get; set; }

        public string? Model { get; set; }

        public int Capacity { get; set; }

        public int RouteId { get; set; }
    }

    public class CreateBusDTO
    {

        public string? Model { get; set; }

        public int Capacity { get; set; }

        public int RouteId { get; set; }
    }
}
