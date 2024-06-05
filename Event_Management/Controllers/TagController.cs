using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public IActionResult GetAllTags()
        {
            var tags = _tagService.GetAllTags();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public IActionResult GetTagById(int id)
        {
            var tag = _tagService.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }
        [HttpPost("event/{eventId}")]
        public IActionResult CreateTagsForEvent(int eventId, [FromBody] IEnumerable<Tag> tags)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tagService.CreateTagsForEvent(eventId, tags);

                    // Map Tag entities to TagDTOs
                    var tagDTOs = tags.Select(t => new TagCreationDTO
                    {
                        EventId = eventId,
                        TagId = t.Id,
                        TagName = t.Name
                    });

                    // Return the DTOs
                    return CreatedAtAction(nameof(GetAllTags), tagDTOs);
                }
                catch (ArgumentException ex)
                {
                    return NotFound(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            _tagService.DeleteTag(id);
            return Ok(new { message = "Tag deleted successfully" });
        }
    }
}
