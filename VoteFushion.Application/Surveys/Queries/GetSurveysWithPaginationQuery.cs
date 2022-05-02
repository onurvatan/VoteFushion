using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using VoteFushion.Application.Common.Interfaces;
using VoteFushion.Application.Common.Mappings;
using VoteFushion.Application.Common.Models;

namespace VoteFushion.Application.Surveys.Queries;

public class GetSurveysWithPaginationQuery : IRequest<PaginatedList<SurveyDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetSurveysWithPaginationQueryHandler : IRequestHandler<GetSurveysWithPaginationQuery, PaginatedList<SurveyDto>>
{
    private readonly IVoteFushionDbContext _context;
    private readonly IMapper _mapper;

    public GetSurveysWithPaginationQueryHandler(IVoteFushionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SurveyDto>> Handle(GetSurveysWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Surveys
            .OrderBy(x => x.Title)
            .ProjectTo<SurveyDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
