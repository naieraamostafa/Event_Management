using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var events = _eventService.GetAllEvents();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            var _event = _eventService.GetEventById(id);
        if (_event == null)
        {
            return NotFound();
        }
        return Ok(_event);

        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] Event eventE)
        {
            if (ModelState.IsValid)
            {
                var createdEvent = _eventService.CreateEvent(eventE);
                return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] Event eventE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingEvent = _eventService.GetEventById(id);
            if (existingEvent == null)
            {
                return NotFound(new { message = "Event not found" });
            }

            _eventService.UpdateEvent(id, eventE);
            return Ok(new { message = "Event updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var existingEvent = _eventService.GetEventById(id);
            if (existingEvent == null)
            {
                return NotFound(new { message = "Event not found" });
            }

            _eventService.DeleteEvent(id);
            return Ok(new { message = "Event deleted successfully" });
        }
    }

}
