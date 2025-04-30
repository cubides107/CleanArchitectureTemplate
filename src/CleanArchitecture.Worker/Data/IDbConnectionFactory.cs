using System.Data.Common;

namespace CleanArchitecture.Worker.Data;
internal interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
