namespace CleanArchitecture.Domain.Common.Exceptions;
public class InvalidFormatException : Exception
{
    public InvalidFormatException(string message)
        : base(message)
    {
    }
}
