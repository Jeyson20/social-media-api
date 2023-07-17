namespace SocialMedia.Domain.Entities.Posts.Events;

public record PostDeletedEvent(int UserId, int PostId) : BaseEvent(Guid.NewGuid());

