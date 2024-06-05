using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AccountController(IUserService userService, IConfiguration configuration, IEmailService emailService)
        {
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                _userService.RegisterUser(registerModel);

                return Ok(new { message = "User registered successfully. Please check your email to confirm your registration." });
            }
            return BadRequest(ModelState);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = _userService.GetUserByConfirmationToken(token);
            if (user == null || user.Id.ToString() != userId)
            {
                return BadRequest(new { message = "Invalid token" });
            }

            await _userService.ConfirmEmailAsync(user, token);
            return Redirect("/email-confirmed.html"); // Redirect to the hosted HTML confirmation page
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (_userService.ValidateUser(loginModel))
            {
                return Ok(new { message = "Login successful" });
            }
            return Unauthorized(new { message = "Invalid login attempt" });
        }
    }
}
