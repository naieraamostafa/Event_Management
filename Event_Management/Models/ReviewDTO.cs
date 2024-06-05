namespace Event_Management.Models
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public int UserId { get; set; }

        public string Comment { get; set; } = " ";
        public int Rating { get; set; } // 1 to 5
    }
}
