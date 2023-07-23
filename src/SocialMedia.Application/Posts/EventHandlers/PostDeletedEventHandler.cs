using MediatR;
using Microsoft.Extensions.Logging;
using SocialMedia.Domain.Events.Posts;

namespace SocialMedia.Application.Posts.EventHandlers
{
    public class PostDeletedEventHandler : INotificationHandler<PostDeletedEvent>
    {
        private readonly ILogger<PostDeletedEventHandler> _logger;
        public PostDeletedEventHandler(ILogger<PostDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(PostDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SocialMedia EventDomain: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
