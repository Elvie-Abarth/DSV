namespace DSV_Book_a_room.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public int RoomId { get; set; }
        public Booking(DateTime startTime, DateTime endTime, int roomId, string description)
        {
            StartTime = startTime;
            EndTime = endTime;
            RoomId = roomId;
            Description = description;
        }
    }
}
