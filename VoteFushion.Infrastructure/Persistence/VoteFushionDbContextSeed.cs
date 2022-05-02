using Microsoft.AspNetCore.Identity;
using VoteFushion.Core.Entities;
using VoteFushion.Core.Enums;

namespace VoteFushion.Infrastructure.Persistence;

public static class VoteFushionDbContextSeed
{
    public static async Task SeedSampleDataAsync(VoteFushionDbContext context)
    {
        if (!context.Surveys.Any())
        {
            context.Surveys.Add(new Survey
            {
                Title = "2022 Survey",
                Votes = new List<Vote>
                 {
                        new Vote {
                            Title = $"How was {DateTime.Now.Year-1}?",
                            VoteItems = new List<VoteItem> {
                                new VoteItem { Title ="Good"},
                                new VoteItem { Title ="Bad"}
                            },
                            VoteType = VoteType.SingleChoice,
                            Note= "Don't overthink"
                 }
                },
                EndDate = DateTime.Now.AddDays(7)
            });

            await context.SaveChangesAsync();
        }
    }
}
