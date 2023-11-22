using API.Context;

namespace API.Models;

public class Edition : Entity
{
    public Book Book { get; set; }
    
    public Status Status { get; set; }
    
    public Department Department { get; set; }
    
    public Borrowing Borrowing { get; set; }

}