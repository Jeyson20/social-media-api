using MediatR;
using Microsoft.Extensions.Logging;
using SocialMedia.Domain.Entities.Users.Events;

namespace SocialMedia.Application.Users.EventHandlers;
public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
	private readonly ILogger<UserCreatedEventHandler> _logger;
	public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
	{
		_logger = logger;
	}
	public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
	{
		_logger.LogInformation("SocialMedia DomainEvent: {DomainEvent}", notification.GetType().Name);

		return Task.CompletedTask;
	}
}


