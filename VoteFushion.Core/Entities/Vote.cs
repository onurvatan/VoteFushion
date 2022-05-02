using VoteFushion.Core.Common;
using VoteFushion.Core.Enums;

namespace VoteFushion.Core.Entities;

public class Vote : AuditableEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Note { get; set; }

    public int Sort { get; set; }

    public VoteType VoteType { get; set; } = VoteType.SingleChoice;

    public List<VoteItem> VoteItems { get; set; } = new List<VoteItem>();
}

