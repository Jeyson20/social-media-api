namespace SocialMedia.Domain.Events.Comments;

public record CommentDeletedEvent(int CommentId) : BaseEvent(Guid.NewGuid());
