﻿namespace Event_Management.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);

    }
}