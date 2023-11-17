using System;
using System.Collections.Generic;
using API.Context;

namespace API.Models;

public partial class Borrowing : Entity
{
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public DateTime DueDate { get; set; }

    public bool Status { get; set; }

    public User Borrower { get; set; }
}
