using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Query.Edition;

public record GetEditionQuery(Guid editionId) : IRequest<EditionDto>;