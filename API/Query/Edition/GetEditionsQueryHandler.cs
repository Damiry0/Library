using API.Context.Repository;
using BooksAPI.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Query.Edition;

public class GetEditionsQueryHandler : IRequestHandler<GetEditionsQuery, IEnumerable<EditionDto>>
{
    private readonly IRepository<API.Models.Edition> _editionRepository;

    public GetEditionsQueryHandler(IRepository<API.Models.Edition> editionRepository)
    {
        _editionRepository = editionRepository;
    }

    public async Task<IEnumerable<EditionDto>> Handle(GetEditionsQuery request, CancellationToken cancellationToken)
    {
        var editions = await _editionRepository.GetAllAsNoTracking()
            .Select(x => new EditionDto())
            .ToListAsync(cancellationToken: cancellationToken);

        return editions;
    }
}