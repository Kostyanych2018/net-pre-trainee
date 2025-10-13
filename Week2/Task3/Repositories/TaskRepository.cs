using Dapper;
using Task3.Entities;
using Task3.Factories;

namespace Task3.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public TaskRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<TaskEntity>> GetAllAsync()
    {
        var sql = "SELECT * FROM Tasks";
        using var connection = _connectionFactory.CreateConnection();
        var tasks = await connection.QueryAsync<TaskEntity>(sql);
        return tasks;
    }

    public async Task<int> AddAsync(TaskEntity taskEntity)
    {
        var sql = @"INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt)
                    OUTPUT INSERTED.id
                    VALUES (@Title, @Description, @IsCompleted, @CreatedAt)";
        using var connection = _connectionFactory.CreateConnection();
        var id = await connection.QuerySingleAsync<int>(sql, taskEntity);
        return id;
    }

    public async Task<bool> UpdateStatusAsync(int id, bool isCompleted)
    {
        var sql = "UPDATE Tasks SET IsCompleted = @IsCompleted WHERE id = @Id";
        using var connection = _connectionFactory.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sql, new { IsCompleted = isCompleted, Id = id });
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sql = "DELETE FROM Tasks WHERE Id = @Id";
        using var connection = _connectionFactory.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows > 0;
    }
}