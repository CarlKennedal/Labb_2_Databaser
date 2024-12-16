using System;
using System.Collections.Generic;

namespace Labb_2_Databaser.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Nationality { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
