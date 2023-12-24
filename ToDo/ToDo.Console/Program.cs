using Microsoft.Data.Sqlite;

string filePath = "todo.sqlite.db";
string connectionString = $"Data Source={filePath}";

Console.WriteLine("Connection String: " + connectionString);

using (var connection = new SqliteConnection(connectionString))
{
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText =
    @"
       SELECT todo_id, description, done
        FROM todo
    ";

    using (var reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var description = reader.GetString(1);
            var done = reader.GetBoolean(2);
            string doneString = done ? "Yes" : "No";
            Console.WriteLine($"ID: {id}, Description: {description}, Done: {doneString}");
        }
    }
}