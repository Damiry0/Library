using System.Text.Json.Serialization;
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

    [JsonConstructor]
    private Borrowing(DateTime borrowDate, DateTime? returnDate, DateTime dueDate, bool isReturned, 
        User user, Edition edition)
    {
        BorrowDate = borrowDate;
        ReturnDate = returnDate;
        DueDate = dueDate;
        IsReturned = isReturned;
        User = user;
        Edition = edition;
    }
    
    public static Borrowing Create(DateTime borrowDate, DateTime? returnDate, DateTime dueDate, bool isReturned, 
        User user, Edition edition)
    {
        return new Borrowing(borrowDate, returnDate, dueDate, isReturned, user, edition);
    }
}