using SendGrid;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Shared
{
    public interface IEmailSender
    {
        Task<Response> SendEmailAsync(string email, string subject, string message);
        Task<Response> Execute(string apiKey, string subject, string message, string email);
    }
}
