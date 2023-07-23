namespace SocialMedia.Domain.Events.Posts;

public record PostDeletedEvent(int UserId, int PostId) : BaseEvent(Guid.NewGuid());

