using Event_Management.Models;

namespace Event_Management.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Ticket> GetTicketsByEventId(int eventId)
        {
            return _context.Tickets.Where(t => t.EventId == eventId).ToList();
        }

        public void BookTicket(int userId, int eventId, TicketDTO ticketDTO)
        {
            var ticket = new Ticket
            {
                UserId = userId,
                EventId = eventId,
                PurchaseDate = ticketDTO.PurchaseDate,
                IsValidated = ticketDTO.IsValidated
            };

            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }
    }
}
