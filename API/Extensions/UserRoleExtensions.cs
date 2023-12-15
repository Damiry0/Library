using BooksAPI.Authentication;

namespace BooksAPI.Extensions;

public static class UserRoleExtensions
{
    public static string ToRoleString(this Role role)
    {
        return Enum.GetName(typeof(Role), role);
    }
}