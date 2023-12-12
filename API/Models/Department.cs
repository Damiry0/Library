using API.Context;

namespace API.Models;

public class Department : Entity
{
    public string Name { get; set; }
    public string Adrress { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    protected Department()
    {
        // EF
    }

    protected Department(string name, string adrress, string email, string phone)
    {
        Name = name;
        Adrress = adrress;
        Email = email;
        Phone = phone;
    }
}