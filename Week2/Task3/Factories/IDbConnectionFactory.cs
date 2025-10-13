using System.Data;

namespace Task3.Factories;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}