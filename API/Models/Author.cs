using API.Context;

namespace API.Models;

public sealed class Author : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string Bio { get; set; }

    public IReadOnlyCollection<Book> Books { get; set; }
}
