namespace VoteFushion.Core.Entities;

public class Survey
{
    public int Id { get; set; }

    public string Title { get; set; } = "Empty";


    public DateTime? EndDate { get; set; }

    public List<Vote> Votes { get; set; } = new List<Vote>();
}

