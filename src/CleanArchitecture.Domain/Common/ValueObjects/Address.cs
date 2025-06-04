namespace CleanArchitecture.Domain.Common.ValueObjects;
public record Address
{
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string Country { get; init; }
    public string ZipCode { get; init; }

    public static Address Create(string Street, string City, string State, string Country, string ZipCode)
    {
        return new Address()
        {
            Street = Street,
            City = City,
            State = State,
            Country = Country,
            ZipCode = ZipCode
        };
    }
}
