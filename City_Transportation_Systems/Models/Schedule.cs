namespace City_Transportation_Systems.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public TimeOnly TimeStamp { get; set; }

        public int Station_Id { get; set; }
        public Station? Station { get; set; }

        public int Route_id { get; set; }
        public Route? Route { get; set; }
    }
}
