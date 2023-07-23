using SocialMedia.Domain.Entities;

namespace SocialMedia.Domain.Events.Likes;

public record LikeCreatedEvent(Like Like) : BaseEvent(Guid.NewGuid());
