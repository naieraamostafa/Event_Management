using Event_Management.Models;

namespace Event_Management.Services
{
    public interface ISearchService
    {
        IEnumerable<Event> SearchEvents(string name, DateTime? date, string location);

    }
}
