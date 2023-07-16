using MediatR;

namespace SocialMedia.Domain.Common;
public record BaseEvent(Guid Id) : INotification;

