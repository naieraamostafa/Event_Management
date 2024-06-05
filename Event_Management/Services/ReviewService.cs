using Event_Management.Models;

namespace Event_Management.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ReviewDTO> GetReviewsByEventId(int eventId)
        {
            return _context.Reviews
                    .Where(r => r.EventId == eventId)
                    .Select(r => new ReviewDTO
                    {
                        Id = r.Id,
                        EventId = r.EventId,
                        UserId = r.UserId,
                        Comment = r.Comment,
                        Rating = r.Rating
                    })
                    .ToList();
        }

        public void AddReview(int eventId, int userId, ReviewDTO reviewDto)
        {
            // Check if the event exists
            var existingEvent = _context.Events.FirstOrDefault(e => e.Id == eventId);
            if (existingEvent == null)
            {
                throw new ArgumentException("Event not found.", nameof(eventId));
            }

            try
            {
                // Create a new Review entity
                var review = new Review
                {
                    EventId = eventId,
                    UserId = userId,
                    Comment = reviewDto.Comment,
                    Rating = reviewDto.Rating
                };

                // Add the review to the context and save changes
                _context.Reviews.Add(review);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding review.", ex);
            }
        }

    }
}
