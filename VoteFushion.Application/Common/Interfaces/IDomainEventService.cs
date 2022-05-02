using VoteFushion.Core.Common;

namespace VoteFushion.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
