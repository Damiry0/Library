using API.Context.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Command.Edition;

public class UpdateEditionCommandHandler : IRequestHandler<UpdateEditionCommand>
{
    private readonly IRepository<API.Models.Edition> _editionRepository;

    public UpdateEditionCommandHandler(IRepository<API.Models.Edition> editionRepository)
    {
        _editionRepository = editionRepository;
    }

    public async Task Handle(UpdateEditionCommand request, CancellationToken cancellationToken)
    {
        var edition = await _editionRepository.GetAllAsNoTracking().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (edition is null)
        {
            throw new Exception("Edition cannot be null");
        }
    }
}