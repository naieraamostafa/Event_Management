using Event_Management.Models;

namespace Event_Management.Services
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAllTags();
        Tag GetTagById(int id);
        IEnumerable<TagCreationDTO> CreateTagsForEvent(int eventId, IEnumerable<Tag> tags);
        void DeleteTag(int id);
    }
}
