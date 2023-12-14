using API.Context;

namespace API.Models;

public class Borrowing : Entity
{
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsReturned { get; set; }

    public User User { get; set; }

    public Edition Edition { get; set; }

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