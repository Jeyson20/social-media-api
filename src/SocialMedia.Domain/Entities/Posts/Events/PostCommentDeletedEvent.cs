namespace SocialMedia.Domain.Entities.Posts.Events;

public record PostCommentDeletedEvent(int UserId, int CommentId) : BaseEvent(Guid.NewGuid());
