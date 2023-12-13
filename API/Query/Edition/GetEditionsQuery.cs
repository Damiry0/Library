using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Query.Edition;

public record GetEditionsQuery() : IRequest<IEnumerable<EditionDto>>;