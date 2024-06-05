using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public string Comment { get; set; } = " ";
        public int Rating { get; set; } // 1 to 5
    }
}
