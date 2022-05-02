using Microsoft.EntityFrameworkCore;
using VoteFushion.Core.Entities;

namespace VoteFushion.Application.Common.Interfaces;

public interface IVoteFushionDbContext
{
    DbSet<Survey> Surveys { get; }
    DbSet<Vote> Votes { get; }
    DbSet<VoteItem> VoteItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
