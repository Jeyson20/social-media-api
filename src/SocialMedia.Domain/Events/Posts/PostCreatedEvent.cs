using SocialMedia.Domain.Entities;

namespace SocialMedia.Domain.Events.Posts;

public record PostCreatedEvent(Post Post) : BaseEvent(Guid.NewGuid());
