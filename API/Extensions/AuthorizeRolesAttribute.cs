using BooksAPI.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace BooksAPI.Extensions;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params Role[] roles)
    {
        Roles = string.Join(",", roles.Select(r => r.ToRoleString()));
    }
}