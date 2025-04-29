﻿namespace CleanArchitecture.Application.Abstractions.Authorization;

public sealed record PermissionsResponse(Guid UserId, HashSet<string> Permissions);
