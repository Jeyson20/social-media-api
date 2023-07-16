namespace SocialMedia.Domain.Entities.Users.Events;

public record UserTokenCreatedEvent(UserToken User) : BaseEvent(Guid.NewGuid());