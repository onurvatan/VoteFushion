using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VoteFushion.Application.Common.Interfaces;
using VoteFushion.Core.Common;
using VoteFushion.Core.Entities;

namespace VoteFushion.Infrastructure.Persistence;

public class VoteFushionDbContext : DbContext, IVoteFushionDbContext
{
    private readonly IDateTime _dateTime;
    private readonly IDomainEventService _domainEventService;


    public VoteFushionDbContext(
        DbContextOptions<VoteFushionDbContext> options,
        IDateTime dateTime,
        IDomainEventService domainEventService) : base(options)
    {
        _dateTime = dateTime;
        _domainEventService = domainEventService;
    }

    public DbSet<Survey> Surveys => Set<Survey>();

    public DbSet<Vote> Votes => Set<Vote>();

    public DbSet<VoteItem> VoteItems => Set<VoteItem>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = "test";
                    entry.Entity.Created = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.CreatedBy = "test";
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }


        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }


}

