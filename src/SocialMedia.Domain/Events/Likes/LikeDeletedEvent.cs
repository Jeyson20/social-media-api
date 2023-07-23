namespace SocialMedia.Domain.Events.Likes;
public record LikeDeletedEvent(int LikeId) : BaseEvent(Guid.NewGuid());
