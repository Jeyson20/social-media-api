namespace SocialMedia.Domain.Entities.Users.Events;
public record UserUpdatedEvent(
	int UserId,
	string FirstName,
	string LastName,
	DateTime DateOfBirth,
	Gender Gender,
	string PhoneNumber) : BaseEvent(Guid.NewGuid());

