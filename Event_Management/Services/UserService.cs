using Event_Management.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
namespace Event_Management.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _configuration;

        private readonly IEmailService _emailService;

        public UserService(ApplicationDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }


        public async void RegisterUser(RegisterModel registerModel)
        {
            var user = new User
            {
                Username = registerModel.Username,
                Email = registerModel.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerModel.Password),
                Role = registerModel.Role,
                ConfirmationToken = Guid.NewGuid().ToString()
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Generate token and encode it
            var tokenBytes = Encoding.UTF8.GetBytes(user.ConfirmationToken);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

            // Construct the confirmation URL
            string confirmationUrl = $"{_configuration["AppUrl"]}/api/account/confirm-email?userId={user.Id}&token={encodedToken}";

            // Send the confirmation email
            string subject = "Registration Confirmation for Our Event Management";
            string body = $"<p>Thank you for registering in our event management system!</p><p>Please confirm your email by clicking <a href='{confirmationUrl}'>here</a>.</p>";
            await _emailService.SendEmailAsync(user.Email, subject, body);
        }
        public bool ValidateUser(LoginModel loginModel)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == loginModel.Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(loginModel.Password, user.PasswordHash))
            {
                return true;
            }
            return false;
        }

        public User GetUserByConfirmationToken(string token)
        {
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            var tokenString = Encoding.UTF8.GetString(decodedToken);
            return _context.Users.SingleOrDefault(u => u.ConfirmationToken == tokenString);
        }

        public async Task ConfirmEmailAsync(User user, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token), "Confirmation token cannot be null or empty.");
            }

            user.IsEmailConfirmed = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
