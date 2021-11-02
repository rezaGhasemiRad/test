using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Mail.Models.Consumers
{
    public class MailConsumer : IConsumer<global::Models.Mail>
    {
        private readonly ILogger<MailConsumer> _logger;

        public MailConsumer(ILogger<MailConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<global::Models.Mail> context)
        {
            _logger.LogInformation(context.Message.Title);
            _logger.LogInformation(context.Message.Message);
        }
    }
}