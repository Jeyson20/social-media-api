using MediatR;
using Microsoft.Extensions.Logging;
using SocialMedia.Domain.Entities.Users.Events;

namespace SocialMedia.Application.Auth.EventHandlers
{
	public class UserTokenCreatedEventHandler : INotificationHandler<UserTokenCreatedEvent>
	{
		private readonly ILogger<UserTokenCreatedEventHandler> _logger;
		public UserTokenCreatedEventHandler(ILogger<UserTokenCreatedEventHandler> logger)
		{
			_logger = logger;
		}
		public Task Handle(UserTokenCreatedEvent notification, CancellationToken cancellationToken)
		{
			_logger.LogInformation("SocialMedia DomainEvent: {DomainEvent}", notification.GetType().Name);
			return Task.CompletedTask;
		}
	}
}
