using Event_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _context.Events.Include(e => e.EventTags).ToList();
        }

        public Event GetEventById(int id)
        {
            return _context.Events.Include(e => e.EventTags).SingleOrDefault(e => e.Id == id);
        }
        public EventDTO CreateEvent(Event eventE)
        {
            _context.Events.Add(eventE);
            _context.SaveChanges();

            // Map Event entity to EventDTO
            var eventDTO = new EventDTO
            {
                Id = eventE.Id,
                Name = eventE.Name, 
                Date = eventE.Date,
                Location = eventE.Location
            };

            return eventDTO;
        }
        public void UpdateEvent(int id, Event eventE)
        {
            var existingEvent = _context.Events.Find(id);
            if (existingEvent != null)
            {
                existingEvent.Name = eventE.Name;
                existingEvent.Date = eventE.Date;
                existingEvent.Location = eventE.Location;
                existingEvent.Description = eventE.Description;
                existingEvent.Category = eventE.Category;

                _context.Events.Update(existingEvent);
                _context.SaveChanges();
            }
        }

        public void DeleteEvent(int id)
    {
        var _event = _context.Events.Find(id);
        if (_event != null)
        {
            _context.Events.Remove(_event);
            _context.SaveChanges();
        }
    }
  }
}
