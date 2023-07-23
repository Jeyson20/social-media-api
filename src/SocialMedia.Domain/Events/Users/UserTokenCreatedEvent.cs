using SocialMedia.Domain.Entities;

namespace SocialMedia.Domain.Events.Users;

public record UserTokenCreatedEvent(UserToken User) : BaseEvent(Guid.NewGuid());