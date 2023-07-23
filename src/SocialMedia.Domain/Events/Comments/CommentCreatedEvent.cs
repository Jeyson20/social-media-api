using SocialMedia.Domain.Entities;

namespace SocialMedia.Domain.Events.Comments;

public record CommentCreatedEvent(Comment Comment) : BaseEvent(Guid.NewGuid());

