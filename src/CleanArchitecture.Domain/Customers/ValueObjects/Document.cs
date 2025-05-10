using System.Text.RegularExpressions;
using CleanArchitecture.Domain.Common.Exceptions;

namespace CleanArchitecture.Domain.Customers.ValueObjects;
public class Document
{
    public DocumentType Type { get; init; }
    public string Value { get; init; }

    public Document(DocumentType type, string value)
    {
        if (!IsValidFormat(type, value))
        {
            throw new InvalidFormatException($"{type} : {value} invalid document format");
        }
        Type = type;
        Value = value;
    }

    private static bool IsValidFormat(DocumentType type, string value)
    {
        return type switch
        {
            DocumentType.CC => Regex.IsMatch(value, @"^\d{10}$"),      // Ej: 12345678
            DocumentType.DNI => Regex.IsMatch(value, @"^\d{8}$"),      // Ej: 12345678
            DocumentType.NIF => Regex.IsMatch(value, @"^[0-9A-Z]{9}$"), // Ej: 12345678A
            DocumentType.Passport => Regex.IsMatch(value, @"^[A-Z0-9]{6,9}$"), // Ej: AB123456
            DocumentType.RUC => Regex.IsMatch(value, @"^\d{11}$"),     // Ej: 12345678901
            DocumentType.CUIT => Regex.IsMatch(value, @"^\d{2}-\d{8}-\d{1}$"), // Ej: 20-12345678-9
            _ => false
        };
    }

    public override string ToString() => $"{Type}: {Value}";
}
