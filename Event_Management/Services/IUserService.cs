using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IUserService
    {
        void RegisterUser(RegisterModel registerModel);
        bool ValidateUser(LoginModel loginModel);
        User GetUserByConfirmationToken(string token);
        Task ConfirmEmailAsync(User user, string token);
    }
}
