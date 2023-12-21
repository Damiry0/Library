namespace BooksAPI.Exceptions;

public class TriggerException : Exception
{
    public TriggerException()
    {
    }

    public TriggerException(string message)
        : base(message)
    {
    }

    public TriggerException(string message, Exception inner)
        : base(message, inner)
    {
    }
}