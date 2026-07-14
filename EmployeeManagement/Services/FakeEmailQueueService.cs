namespace EmployeeManagement.Services
{
    public class FakeEmailQueueService
    {
        private readonly Queue<string> _emails = new();

        public FakeEmailQueueService()
        {
            _emails.Enqueue("alice@example.com");
            _emails.Enqueue("bob@example.com");
            _emails.Enqueue("charlie@example.com");
        }

        public string? GetNextEmail()
        {
            if (_emails.Count == 0)
                return null;

            return _emails.Dequeue();
        }
    }
}