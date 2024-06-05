using Azure;

namespace Event_Management.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = " ";
        public DateTime Date { get; set; }
        public string Location { get; set; } = " ";
        public string Description { get; set; } = " ";
        public string Category { get; set; } = " ";

        public ICollection<EventTag> EventTags { get; set; } = new List<EventTag>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

}
