using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("event/{eventId}")]
        public IActionResult GetReviewsByEventId(int eventId)
        {
            var reviews = _reviewService.GetReviewsByEventId(eventId);
            return Ok(reviews);
        }

        [HttpPost("event/{eventId}/user/{userId}")]
        public IActionResult AddReview(int eventId, int userId, [FromBody] ReviewDTO reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _reviewService.AddReview(eventId, userId, reviewDto);
            return CreatedAtAction(nameof(GetReviewsByEventId), new { eventId }, reviewDto);
        }
    }
}
