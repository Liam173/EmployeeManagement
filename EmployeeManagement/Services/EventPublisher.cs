using EmployeeManagement.Interfaces;

namespace EmployeeManagement.Services
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EventPublisher> _logger;

        public EventPublisher(
            IServiceProvider serviceProvider,
            ILogger<EventPublisher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task PublishAsync<TEvent>(TEvent @event)
        {
            var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

            var tasks = handlers.Select(async handler =>
            {
                try
                {
                    _logger.LogInformation(
                        "Executing {Handler}.",
                        handler.GetType().Name);

                    await handler.HandleAsync(@event);

                    _logger.LogInformation(
                        "{Handler} completed successfully.",
                        handler.GetType().Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "{Handler} failed.",
                        handler.GetType().Name);
                }
            });

            await Task.WhenAll(tasks);
        }
    }
}
