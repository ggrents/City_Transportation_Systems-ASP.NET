namespace City_Transportation_Systems.Models
{
    public class Driver
    {
        public int Id { get; set; }

        public string? First_name { get; set; }

        public string? Last_name { get; set; }

        public int BusId { get; set; }
        
        public Bus? Bus {  get; set; }
    }
}
