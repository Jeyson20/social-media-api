namespace SocialMedia.Domain.Entities.Users.Events;

public record UserTokenUpdatedEvent(
	int UserId,
	string Token,
	DateTime Expiration) : BaseEvent(Guid.NewGuid());

