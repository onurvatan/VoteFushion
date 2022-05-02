using VoteFushion.Application.Common.Interfaces;

namespace VoteFushion.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
