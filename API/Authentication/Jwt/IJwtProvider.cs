using API.Models;

namespace BooksAPI.Authentication.Jwt;

public interface IJwtProvider
{
    string GenerateToken(User user);
}