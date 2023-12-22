using API.Context;

namespace API.Models;

public class User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }

    private List<UserRole> _userRoles = new();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public string StudentNumber { get; set; }
    public Department Department { get; set; }

    private User()
    {
        // EF
    }

    private User(string firstName, string lastName, string email, string studentNumber, Department department)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        StudentNumber = studentNumber;
        Department = department;
    }

    public static User Create(string firstName, string lastName, string email, string studentNumber,
        Department department)
    {
        return new User(firstName, lastName, email, studentNumber, department);
    }

    public void Update(string firstName, string lastName, string email, string studentNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        StudentNumber = studentNumber;
    }
}