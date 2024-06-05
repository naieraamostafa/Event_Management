using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime PurchaseDate { get; set; }
        public bool IsValidated { get; set; }
    }
}
