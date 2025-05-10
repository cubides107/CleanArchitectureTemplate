using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Customers.ValueObjects;

namespace CleanArchitecture.Domain.Customers.Entities;
public class Customer : Entity
{
    public readonly static Customer Customer1 = new()
    {
        Id = Guid.Parse("96633443-98ec-4362-8655-678f1e228aef"),
        FirstName = "Cristian",
        LastName = "Cubides",
        Email = new Email("cristhiancubides84@gmail.com"),
        PhoneNumber = "3176718273",
        IdentityDocument = new Document(DocumentType.DNI, "1007420164"),
        Address = new Address()
        {
            City = "Tunja",
            Country = "Colombia", 
            State = "Boyaca",
            Street = "Carrera 1 este #33-34",
            ZipCode = "10001"
        },
        RegistrationDate = new DateTime(2025,05,09,10,10,10, DateTimeKind.Utc),
        IsActive = true,
    };

    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public Document IdentityDocument { get; private set; }
    public Address Address { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public bool IsActive { get; private set; }

    private Customer()
    {
    }

    public static Customer Create(string FirstName, string LastName, Email Email,
        string PhoneNumber, Document IdentityDocument, Address Address)
    {
        return new Customer()
        {
            Id = Guid.NewGuid(),
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Address = Address,
            IdentityDocument = IdentityDocument,
            RegistrationDate = DateTime.UtcNow,
            IsActive = true
        };
    }
}
