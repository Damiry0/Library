using API.Context;

namespace API.Models;

public class Department : Entity
{
    public string Name { get; set; }
    public string Adrress { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}