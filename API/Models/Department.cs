using System.Text.Json.Serialization;
using API.Context;
using BooksAPI.Enums;

namespace API.Models;

public class Department : Entity
{
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }

    public DataCenter DataCenter { get; private set; }
    
    private Department()
    {
        // EF
    }
    
    [JsonConstructor]
    private Department(string name, string address, string email, string phone, DataCenter dataCenter)
    {
        Name = name;
        Address = address;
        Email = email;
        Phone = phone;
        DataCenter = dataCenter;
    }

    public static Department Create(string name, string address, string email, string phone, DataCenter dataCenter)
    {
        return new Department(name, address, email, phone, dataCenter);
    }
}