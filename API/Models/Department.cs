﻿using API.Context;
using BooksAPI.Enums;

namespace API.Models;

public class Department : Entity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public DataCenter DataCenter { get; set; }

    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    private Department()
    {
        // EF
    }

    private Department(string name, string address, string email, string phone)
    {
        Name = name;
        Address = address;
        Email = email;
        Phone = phone;
    }

    public static Department Create(string name, string address, string email, string phone)
    {
        return new Department(name, address, email, phone);
    }
}