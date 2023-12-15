using API.Context.Repository;
using BooksAPI.Exceptions;
using MediatR;

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
        var edition = _editionRepository.GetAllAsNoTracking()
            .FirstOrDefault(x => x.Id == request.Id);

        if (edition is null)
        {
            throw new NotFoundException("Edition not found.");
        }
    }
}