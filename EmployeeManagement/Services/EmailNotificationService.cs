using EmployeeManagement.Interfaces;

namespace EmployeeManagement.Services
{
    public class EmailNotificationService : INotificationService
    {
        public void SendNotification(string message)
        {
            // Simulate sending an email notification
            Console.WriteLine($"Email notification sent: {message}");
        }
    }
}
