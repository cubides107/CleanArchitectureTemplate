﻿namespace CleanArchitecture.SharedKernel;
public interface IDomainEvent
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
