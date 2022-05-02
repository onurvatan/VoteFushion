

using VoteFushion.Application.Common.Mappings;
using VoteFushion.Core.Entities;

namespace VoteFushion.Application.Surveys.Queries;

public class SurveyDto : IMapFrom<Survey>
{
    public int Id { get; set; }
    public string Title { get; set; }
}

