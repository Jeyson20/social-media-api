using MediatR;
using Microsoft.Extensions.Logging;
using SocialMedia.Domain.Entities.Users.Events;

namespace SocialMedia.Application.Auth.EventHandlers
{
	public class UserTokenUpdatedEventHandler : INotificationHandler<UserTokenUpdatedEvent>
	{
		private readonly ILogger<UserTokenUpdatedEventHandler> _logger;
		public UserTokenUpdatedEventHandler(ILogger<UserTokenUpdatedEventHandler> logger)
		{
			_logger = logger;
		}
		public Task Handle(UserTokenUpdatedEvent notification, CancellationToken cancellationToken)
		{
			_logger.LogInformation("SocialMedia DomainEvent: {DomainEvent}", notification.GetType().Name);
			return Task.CompletedTask;
		}
	}
}
