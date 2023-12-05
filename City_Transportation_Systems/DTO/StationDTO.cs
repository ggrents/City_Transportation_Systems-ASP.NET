namespace City_Transportation_Systems.DTO
{
    public class StationDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }
    }

    public class CreateStationDTO
    {
        public string? Name { get; set; }
    }
}
