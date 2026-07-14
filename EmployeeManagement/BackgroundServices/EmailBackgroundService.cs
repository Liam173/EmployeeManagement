using EmployeeManagement.Services;

namespace EmployeeManagement.BackgroundServices
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly ILogger<EmailBackgroundService> _logger;

        private readonly FakeEmailQueueService _queue;

        public EmailBackgroundService(
            ILogger<EmailBackgroundService> logger,
            FakeEmailQueueService queue)
        {
            _logger = logger;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Email Background Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var email = _queue.GetNextEmail();

                if (email == null)
                {
                    _logger.LogInformation(
                        "No emails waiting.");
                }
                else
                {
                    _logger.LogInformation(
                        "Sending email to {Email}.",
                        email);
                }

                await Task.Delay(
                    TimeSpan.FromSeconds(15),
                    stoppingToken);
            }

            _logger.LogInformation(
                "Email Background Service stopped.");
        }
    }
}