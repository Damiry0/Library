using API.Context;

namespace API.Models;

public class User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<Borrowing> Borrowings { get; set; }
    
    public Department Department { get; set; }
}
