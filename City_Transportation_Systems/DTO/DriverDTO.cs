namespace City_Transportation_Systems.DTO
{
    public class CreateDriverDTO
    {
        public string? First_name { get; set; }

        public string? Last_name { get; set; }

        public int BusId { get; set; }

    }
    
    public class DriverDTO : CreateDriverDTO
    {
        public int Id { get; set; }
    }
}
