using SocialMedia.Domain.Entities;

namespace SocialMedia.Domain.Events.Users;
public record UserCreatedEvent(User User) : BaseEvent(Guid.NewGuid());

