using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IReviewService
    {
        IEnumerable<ReviewDTO> GetReviewsByEventId(int eventId);
        void AddReview(int eventId, int userId, ReviewDTO reviewDto);
    }
}
