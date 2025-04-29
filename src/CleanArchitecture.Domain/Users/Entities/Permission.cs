namespace CleanArchitecture.Domain.Users.Entities;

public sealed class Permission
{
    public static readonly Permission GetUser = new("users:read");
    public static readonly Permission CreateUser = new("users:create");
    public string Code { get; }

    public Permission(string code)
    {
        Code = code;
    }
}
