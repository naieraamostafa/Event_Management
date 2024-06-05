using Event_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _context.Tags.Include(t => t.EventTags).ToList();
        }

        public Tag GetTagById(int id)
        {
            return _context.Tags.Include(t => t.EventTags).FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TagCreationDTO> CreateTagsForEvent(int eventId, IEnumerable<Tag> tags)
        {
            var eventEntity = _context.Events.Find(eventId);
            if (eventEntity != null)
            {
                var tagDTOs = new List<TagCreationDTO>();

                foreach (var tag in tags)
                {
                    // Create EventTag entity
                    var eventTag = new EventTag
                    {
                        Event = eventEntity,
                        Tag = tag
                    };

                    // Add tag to context
                    _context.Tags.Add(tag);

                    // Add eventTag to context
                    _context.EventTags.Add(eventTag);

                    tagDTOs.Add(new TagCreationDTO
                    {
                        EventId = eventId,
                        TagId = tag.Id,
                        TagName = tag.Name
                    });
                }

                // Save changes once after adding all tags and eventTag
                _context.SaveChanges();

                return tagDTOs;
            }
            else
            {
                throw new ArgumentException("Event not found");
            }
        }

        public void DeleteTag(int id)
        {
            var tag = _context.Tags.Find(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
            }
        }
    }
}
