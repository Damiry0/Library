using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Command.Borrowing;

public record CreateBorrowingCommand(BorrowingDto BorrowingDto, Guid UserId, Guid EditionId): IRequest;