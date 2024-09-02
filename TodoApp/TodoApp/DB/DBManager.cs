using Microsoft.Data.Sqlite;
using TodoApp.Models;

namespace TodoApp.DB;

public class DBManager
{
    private const string ConnectionString = "Data Source=todos.db";

    public static int Insert(Todo todo)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        var insertCmd = connection.CreateCommand();

        insertCmd.CommandText = """
            INSERT INTO Todo (IsCompleted, Description, DueDate) 
            VALUES (@isCompleted, @description, @dueDate);
        """;

        insertCmd.Parameters.AddWithValue("@isCompleted", todo.IsCompleted);
        insertCmd.Parameters.AddWithValue("@description", todo.Description);
        insertCmd.Parameters.AddWithValue("@dueDate", todo.DueDate);

        var result = insertCmd.ExecuteNonQuery();
        return result;
    }

    public static void MaybeInitTable()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var createTableCmd = connection.CreateCommand();
        createTableCmd.CommandText = """
            CREATE TABLE IF NOT EXISTS Todo (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                IsCompleted BOOLEAN,
                Description TEXT,
                DueDate TEXT
            );
        """;
        createTableCmd.ExecuteNonQuery();
    }

    public static List<Todo> QueryTodos()
    {
        var todos = new List<Todo>();

        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var queryCmd = connection.CreateCommand();
        queryCmd.CommandText = "SELECT * FROM Todo;";

        using var reader = queryCmd.ExecuteReader();
        while (reader.Read())
        {
            todos.Add(new Todo
            {
                ID = reader.GetInt32(0),
                IsCompleted = reader.GetInt32(1),
                Description = reader.GetString(2),
                DueDate = reader.GetString(3)
            });
        }

        return todos;
    }

    // public static int Update(Todo todo)
    // {
    //     
    // }
    //
    
    public static int Delete(int? todoID)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var queryCmd = connection.CreateCommand();
        queryCmd.CommandText = """
            DELETE FROM Todo
            WHERE ID = @ID;
        """;

        queryCmd.Parameters.AddWithValue("@ID", todoID);

        var result = queryCmd.ExecuteNonQuery();
        return result;
    }

    public static void Clear()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var queryCmd = connection.CreateCommand();
        queryCmd.CommandText = "DROP TABLE Todo ;";
        queryCmd.ExecuteNonQuery();
    }
}