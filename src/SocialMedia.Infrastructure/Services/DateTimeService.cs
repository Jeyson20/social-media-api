using SocialMedia.Application.Common.Interfaces;

namespace SocialMedia.Infrastructure.Services
{
	internal class DateTimeService : IDateTime
	{
		public DateTime Now => DateTime.Now;
	}
}
