using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IEventService
    {
        IEnumerable<Event> GetAllEvents();
        Event GetEventById(int id);
        EventDTO CreateEvent(Event eventE);
        void UpdateEvent(int id, Event eventE);
        void DeleteEvent(int id);
        
    }

}
