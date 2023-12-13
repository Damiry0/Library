using API.Context;
using FluentValidation;

namespace API.Models;

public class Book : Entity
{
    public string Title { get; private set; }
    public DateTime PublicationDate { get; private set; }
    public string Isbn { get; private set; }
    public int Pages { get; private set; }
    public int Amount { get; private set; }
    public string Description { get; private set; }

    private readonly List<Author> _authors = new();
    public IReadOnlyCollection<Author> Authors => _authors;

    private readonly List<Edition> _editions = new();
    public IReadOnlyCollection<Edition> Editions => _editions;


    public static Book Create(string title, DateTime publicationDate, string isbn, int pages, int amount,
        string description,
        IEnumerable<Author> authors, IEnumerable<Edition> editions)
    {
        return new Book(title, publicationDate, isbn, pages, amount, description, authors, editions);
    }

    private Book()
    {
        // EF
    }

    private Book(string title, DateTime publicationDate, string isbn, int pages, int amount, string description,
        IEnumerable<Author> authors, IEnumerable<Edition> editions)
    {
        Title = title;
        PublicationDate = publicationDate;
        Isbn = isbn;
        Pages = pages;
        Amount = amount;
        Description = description;
        _authors = authors.ToList();
        _editions = editions.ToList();

        Validate();
    }

    private void Validate()
    {
        var validator = new BookValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }

    private class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.PublicationDate).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Pages).NotEmpty();
            RuleFor(x => x.Isbn).NotEmpty();
        }
    }
}