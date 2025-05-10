using System.IO;
using System.Text.RegularExpressions;
using CleanArchitecture.Domain.Common.Exceptions;
using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Domain.Customers.ValueObjects;
public class Email : ValueObject
{
    private const int MaxLength = 254;

    private static readonly Regex EmailRegex = new(@"^[\w\.-]+@[\w\.-]+\.\w+$", RegexOptions.Compiled);

    public string Value { get; init; }

    public Email(string email)
    {
        ValidateEmail(email);
        Value = email;
    }

    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidFormatException($"{nameof(Email)} cannot be null or empty.");
        }

        if (email.Length > MaxLength)
        {
            throw new InvalidFormatException($"{nameof(Email)} cannot exceed {MaxLength} characters.");
        }

        if (!EmailRegex.IsMatch(email))
        {
            throw new InvalidFormatException($"{nameof(Email)} invalid email format.");
        }
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
