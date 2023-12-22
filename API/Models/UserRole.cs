using API.Context;
using BooksAPI.Authentication;

namespace API.Models;

public class UserRole : Entity
{
    public Role Role { get; set; }

    private UserRole()
    {
        // EF
    }

    public UserRole(Role role)
    {
        Role = role;
    }
}