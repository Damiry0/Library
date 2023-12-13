using API.Context.Repository;
using BooksAPI.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Query.Edition;

public class GetEditionQueryHandler : IRequestHandler<GetEditionQuery, EditionDto>
{
    private readonly IRepository<API.Models.Edition> _editionRepository;

    public GetEditionQueryHandler(IRepository<API.Models.Edition> editionRepository)
    {
        _editionRepository = editionRepository;
    }

    public async Task<EditionDto> Handle(GetEditionQuery request, CancellationToken cancellationToken)
    {
        var edition = await _editionRepository.GetAllAsNoTracking().Where(x => x.Id == request.editionId)
            .Select(x => new EditionDto())
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return edition;
    }
}