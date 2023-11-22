using System;
using System.Collections.Generic;
using API.Context;

namespace API.Models;

public sealed class Book : Entity
{
    public string Title { get; set; }
    
    public DateTime? PublicationDate { get; set; }

    public string Isbn { get; set; }

    public int Pages { get; set; }
    
    public int Amount { get; set; }
    
    public string Description { get; set; }
    
    public ICollection<Author> Authors { get; set; }
    
    public ICollection<Edition> Editions { get; set; }
    
    
}
