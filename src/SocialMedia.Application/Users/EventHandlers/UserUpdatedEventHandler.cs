using MediatR;
using Microsoft.Extensions.Logging;
using SocialMedia.Domain.Entities.Users.Events;

namespace SocialMedia.Application.Users.EventHandlers
{
	public class UserUpdatedEventHandler : INotificationHandler<UserUpdatedEvent>
	{
		private readonly ILogger<UserUpdatedEventHandler> _logger;
		public UserUpdatedEventHandler(ILogger<UserUpdatedEventHandler> logger)
		{
			_logger = logger;
		}
		public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
		{
			_logger.LogInformation("SocialMedia DomainEvent: {DomainEvent}", notification.GetType().Name);
			return Task.CompletedTask;
		}
	}
}
