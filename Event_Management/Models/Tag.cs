using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = " ";


        public ICollection<EventTag> EventTags { get; set; } = new List<EventTag>();

    }

}
