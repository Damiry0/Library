using API.Context;

namespace API.Models;

public class Edition : Entity
{
    public Status Status { get; private set; }
    public Book Book { get; private set; }
    public Department Department { get; private set; }

    public static Edition Create(Book book, Department department)
    {
        return new Edition(book, department);
    }
    
    private Edition()
    {
        // EF
    }

    private Edition(Book book, Department department)
    {
        Status = Status.Available;
        Book = book;
        Department = department;
    }

    public void ChangeBookStatus(Status status)
    {
        Status = status;
    }
    
}