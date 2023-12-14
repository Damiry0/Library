using API.Context;

namespace API.Models;

public class Borrowing : Entity
{
    public DateTime BorrowDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public bool IsReturned { get; private set; }

    public User User { get; private set; }

    public Edition Edition { get; private set; }

    private Borrowing()
    {
        // EF
    }

    private Borrowing(DateTime borrowDate, DateTime? returnDate, DateTime dueDate, bool isReturned, User user)
    {
        BorrowDate = borrowDate;
        ReturnDate = returnDate;
        DueDate = dueDate;
        IsReturned = isReturned;
        User = user;
    }
    
    public static Borrowing Create(DateTime borrowDate, DateTime? returnDate, DateTime dueDate, bool isReturned, User user)
    {
        return new Borrowing(borrowDate, returnDate, dueDate, isReturned, user);
    }
}