using FluentValidation;

namespace BooksAPI.Authentication;

public class Email
{
    public string Address { get; private set; }

    private Email(string address)
    {
        Address = address;

        Validate();
    }


    public static Email Create(string email)
    {
        return new Email(email);
    }

    private void Validate()
    {
        var validator = new EmailValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }

    private class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Address).EmailAddress().WithMessage("Email is invalid.");
        }
    }
}