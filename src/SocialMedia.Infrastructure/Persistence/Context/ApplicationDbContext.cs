﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Domain.Entities;
using SocialMedia.Infrastructure.Common;
using SocialMedia.Infrastructure.Persistence.Interceptors;
using System.Reflection;

namespace SocialMedia.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		private readonly IMediator _mediator;
		private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
			AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
			IMediator mediator) : base(options)
		{
			_auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
			_mediator = mediator;
		}
		public DbSet<User> Users => Set<User>();
		public DbSet<Post> Posts => Set<Post>();
		public DbSet<Comment> Comments => Set<Comment>();
		public DbSet<Like> Likes => Set<Like>();

		public DbSet<UserToken> UserTokens => Set<UserToken>();

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
		}


		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _mediator.DispatchDomainEvents(this);
			return await base.SaveChangesAsync(cancellationToken);
		}

		public override void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				base.Dispose();
			}
		}
	}
}
