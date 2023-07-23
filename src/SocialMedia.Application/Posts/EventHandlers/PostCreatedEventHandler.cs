using MediatR;
using Microsoft.Extensions.Logging;
using SocialMedia.Domain.Events.Posts;

namespace SocialMedia.Application.Posts.EventHandlers
{
    public class PostCreatedEventHandler : INotificationHandler<PostCreatedEvent>
	{
		private readonly ILogger<PostCreatedEventHandler> _logger;
		public PostCreatedEventHandler(ILogger<PostCreatedEventHandler> logger)
		{
			_logger = logger;
		}

		public Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
		{
			_logger.LogInformation("SocialMedia DomainEvent: {DomainEvent}", notification.GetType().Name);

			return Task.CompletedTask;
		}
	}
}
