using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("book/{userId}/{eventId}")]
        public IActionResult BookTicket(int userId, int eventId, [FromBody] TicketDTO ticketDTO)
        {
            if (ModelState.IsValid)
            {
                _ticketService.BookTicket(userId, eventId, ticketDTO);
                return Ok(new { message = "Ticket booked successfully" });
            }
            return BadRequest(ModelState);
        }
    }
}
