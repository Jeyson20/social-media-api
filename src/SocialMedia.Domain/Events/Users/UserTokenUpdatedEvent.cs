namespace SocialMedia.Domain.Events.Users;

public record UserTokenUpdatedEvent(
    int UserId,
    string Token,
    DateTime Expiration) : BaseEvent(Guid.NewGuid());

