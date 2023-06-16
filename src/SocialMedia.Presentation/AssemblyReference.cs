using System.Reflection;

namespace SocialMedia.Presentation
{
	public class AssemblyReference
	{
		public static Assembly Assembly { get; } = typeof(AssemblyReference).Assembly;
	}
}