namespace SocialMedia.Domain.Events.Users;

public record UserDeletedEvent(int UserId) : BaseEvent(Guid.NewGuid());
