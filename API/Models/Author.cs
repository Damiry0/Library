using API.Context;
using FluentValidation;

namespace API.Models;

public sealed class Author : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Bio { get; private set; }


    private readonly List<Book> _books = new();
    public IReadOnlyCollection<Book> Books => _books;

    private Author(string firstName, string lastName, string bio)
    {
        FirstName = firstName;
        LastName = lastName;
        Bio = bio;

        Validate();
    }

    public Author()
    {
        // EF
    }

    private void Validate()
    {
        var validator = new AuthorValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }

    private class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Bio).NotEmpty().WithMessage("Bio is required.");
        }
    }
}