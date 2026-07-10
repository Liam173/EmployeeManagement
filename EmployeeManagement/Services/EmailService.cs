using EmployeeManagement.Settings;
using Microsoft.Extensions.Options;

namespace EmployeeManagement.Services
{
    public class EmailService
    {
        private readonly IOptions<EmailSettings> _options;

        public EmailService(IOptions<EmailSettings> options)
        {
            _options = options;
        }

        public string GetStoredEmailAddress() 
        {
            return _options.Value.Username;
        }
    }
}
