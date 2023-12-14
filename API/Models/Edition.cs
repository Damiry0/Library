using API.Context;

namespace API.Models;

public class Edition : Entity
{
    public Status Status { get; set; }
    public Book Book { get; set; }
    public Department Department { get; set; }

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
}