using API.Context;

namespace API.Models;

public class User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }

    public string StudentNumber { get; set; }
    public Department Department { get; set; }

    private readonly List<Borrowing> _borrowings = new();
    public List<Borrowing> Borrowings => _borrowings;


    public User()
    {
        // EF
    }

    private User(string firstName, string lastName, Department department)
    {
        FirstName = firstName;
        LastName = lastName;
        Department = department;
    }
}