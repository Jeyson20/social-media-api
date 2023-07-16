namespace SocialMedia.Domain.Entities.Users.Events;

public record UserDeletedEvent(int UserId) : BaseEvent(Guid.NewGuid());
