using System.Security.Cryptography;
using System.Text;

namespace SocialMedia.Application.Common.Helpers
{
	public static class PasswordHelper
	{
		public static string HashPassword(string password)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			byte[] hash = SHA256.HashData(bytes);
			return Convert.ToBase64String(hash);
		}

		public static bool VerifyPassword(string password, string hashedPassword)
		{
			string hashedInput = HashPassword(password);
			return string.Equals(hashedInput, hashedPassword);
		}

		public static string GeneratePassword(int length)
		{
			const string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
			StringBuilder password = new();
			Random random = new();

			for (int i = 0; i < length; i++)
			{
				int index = random.Next(0, allowedCharacters.Length);
				password.Append(allowedCharacters[index]);
			}

			return password.ToString();
		}
	}
}
