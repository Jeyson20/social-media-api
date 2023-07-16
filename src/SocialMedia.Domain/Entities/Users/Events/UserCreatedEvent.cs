namespace SocialMedia.Domain.Entities.Users.Events;
public record UserCreatedEvent(User User) : BaseEvent(Guid.NewGuid());

