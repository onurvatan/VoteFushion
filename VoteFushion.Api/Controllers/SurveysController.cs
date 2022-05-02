using Microsoft.AspNetCore.Mvc;
using VoteFushion.Application.Common.Models;
using VoteFushion.Application.Surveys.Queries;

namespace VoteFushion.Api.Controllers;

public class SurveysController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<SurveyDto>>> GetSurveys([FromQuery] GetSurveysWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }
}

