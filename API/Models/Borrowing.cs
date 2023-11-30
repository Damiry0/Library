using API.Context;

namespace API.Models;

public class Borrowing : Entity
{
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool Status { get; set; }

    public User User { get; set; }

    private readonly List<Edition> _editions = new();
    public IReadOnlyCollection<Edition> Editions => _editions;

    public Borrowing()
    {
        // EF
    }

    public Borrowing(DateTime borrowDate, DateTime? returnDate, DateTime dueDate, bool status, User user)
    {
        BorrowDate = borrowDate;
        ReturnDate = returnDate;
        DueDate = dueDate;
        Status = status;
        User = user;
    }
}