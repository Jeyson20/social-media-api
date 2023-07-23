using MediatR;
using Microsoft.Extensions.Logging;
using SocialMedia.Domain.Events.Users;

namespace SocialMedia.Application.Users.EventHandlers
{
    public class UserDeletedEventHandler : INotificationHandler<UserDeletedEvent>
	{
		private readonly ILogger<UserDeletedEventHandler> _logger;
		public UserDeletedEventHandler(ILogger<UserDeletedEventHandler> logger)
		{
			_logger = logger;
		}
		public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
		{
			_logger.LogInformation("SocialMedia DomainEvent: {DomainEvent}", notification.GetType().Name);
			return Task.CompletedTask;
		}
	}
}
