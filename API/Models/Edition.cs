using API.Context;

namespace API.Models;

public class Edition : Entity
{
    public Status Status { get; set; }
    public Book Book { get; set; }
    public Department Department { get; set; }
    public Borrowing Borrowing { get; set; }

    public Edition()
    {
        // EF
    }

    public static Edition Create(Book book, Department department, Borrowing borrowing)
    {
        return new Edition(book, department, borrowing);
    }


    public Edition(Book book, Department department, Borrowing borrowing)
    {
        Status = Status.Available;
        Book = book;
        Department = department;
        Borrowing = borrowing;
    }
}