namespace Event_Management.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; } // Admin, EventManager, Attendee
        public string ConfirmationToken { get; set; } = " ";
        public bool IsEmailConfirmed { get; set; } = false;
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }

}
