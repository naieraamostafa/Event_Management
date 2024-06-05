using Event_Management.Models;

namespace Event_Management.Services
{
    public interface ITicketService
    {
        IEnumerable<Ticket> GetTicketsByEventId(int eventId);
        void BookTicket(int userId, int eventId, TicketDTO ticketDTO);
    }
}
