namespace SocialMedia.Domain.Entities.Posts.Events;

public record PostCreatedEvent(Post Post) : BaseEvent(Guid.NewGuid());
