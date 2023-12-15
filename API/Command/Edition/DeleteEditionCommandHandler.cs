using API.Context.Repository;
using BooksAPI.Exceptions;
using MediatR;

namespace BooksAPI.Command.Edition;

public class DeleteEditionCommandHandler : IRequestHandler<DeleteEditionCommand>
{
    private readonly IRepository<API.Models.Edition> _editionRepository;

    public DeleteEditionCommandHandler(IRepository<API.Models.Edition> editionRepository)
    {
        _editionRepository = editionRepository;
    }

    public async Task Handle(DeleteEditionCommand request, CancellationToken cancellationToken)
    {
        var edition = _editionRepository.GetAllAsNoTracking()
            .FirstOrDefault(x => x.Id == request.Id);

        if (edition is null)
        {
            throw new NotFoundException("Edition not found.");
        }

        _editionRepository.Delete(edition, edition.Department.DataCenter);
        await _editionRepository.SaveAsync(edition.Department.DataCenter);
    }
}