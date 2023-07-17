namespace SocialMedia.Domain.Entities.Posts.Events;

public record PostCommentAddedEvent(Comment Comment) : BaseEvent(Guid.NewGuid());

