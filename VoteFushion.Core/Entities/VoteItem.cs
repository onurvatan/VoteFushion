using VoteFushion.Core.Common;

namespace VoteFushion.Core.Entities;

public class VoteItem : AuditableEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
}

