using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Shared.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
