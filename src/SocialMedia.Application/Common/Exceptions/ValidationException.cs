﻿using FluentValidation.Results;

namespace SocialMedia.Application.Common.Exceptions
{
	public class ValidationException : Exception
	{
		public IDictionary<string, string[]> Errors { get; }
		public ValidationException() : base("One or more validation failures have orcurred")
		{
			Errors = new Dictionary<string, string[]>();
		}

		public ValidationException(IEnumerable<ValidationFailure> failures)
			: this()
		{
			Errors = failures
				.GroupBy(e => e.PropertyName.ToLowerInvariant(), e => e.ErrorMessage)
				.ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
		}
	}
}
