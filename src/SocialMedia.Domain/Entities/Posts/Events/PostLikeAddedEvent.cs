namespace SocialMedia.Domain.Entities.Posts.Events;

public record PostLikeAddedEvent(Like Like) : BaseEvent(Guid.NewGuid());
