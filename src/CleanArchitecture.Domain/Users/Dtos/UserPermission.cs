namespace CleanArchitecture.Domain.Users.Dtos;
public class UserPermission
{
    public Guid UserId { get; set; }

    public string Permissions { get; set; }
}
