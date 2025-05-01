using System.Data.Common;

namespace CleanArchitecture.Infrastructure.Data;
public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
