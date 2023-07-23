using MediatR;
using Microsoft.Extensions.Logging;
using SocialMedia.Domain.Events.Users;

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
