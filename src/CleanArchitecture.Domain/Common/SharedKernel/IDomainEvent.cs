using System.Text.Json.Serialization;
using CleanArchitecture.Domain.Orders.Events;
using CleanArchitecture.Domain.Users.Events;
using MediatR;

namespace CleanArchitecture.Domain.Common.SharedKernel;

[JsonDerivedType(typeof(OrderCreatedDomainEvent), typeDiscriminator: "OrderCreatedDomainEvent")]
[JsonDerivedType(typeof(UserRegisteredDomainEvent), typeDiscriminator: "UserRegisteredDomainEvent")]
public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
