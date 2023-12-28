using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using ToDo.Shared;

namespace ToDo.Api
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly string? _connectionString;
        private readonly SqliteConnection _connection;

        public ToDoRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ToDoDatabase");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("Connection string is null or empty.");
            }
            _connection = new SqliteConnection(_connectionString);
        }

        //~ToDoRepository()
        //{
        //    Console.WriteLine("REPO: Disposing");
        //}

        public async Task<ToDoItem> AddToDoAsync(ToDoItem toDoItem)
        {
            _connection.Open();

            var command = _connection.CreateCommand();
            command.CommandText =
                @"
                INSERT INTO todo (description, done)
                VALUES (@description, @done);
                SELECT last_insert_rowid();";
            command.Parameters.AddWithValue("@description", toDoItem.Description);
            command.Parameters.AddWithValue("@done", toDoItem.IsComplete);
            var id = await command.ExecuteScalarAsync();
            toDoItem.Id = Convert.ToInt32(id);

            _connection.Close();
            return toDoItem;
        }

        public async Task DeleteToDoAsync(int id)
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            command.CommandText =
                @"
                DELETE FROM todo
                WHERE todo_id = @id";
            command.Parameters.AddWithValue("@id", id);
            await command.ExecuteNonQueryAsync();

            _connection.Close();
        }

        [return: MaybeNull]
        public async Task<ToDoItem> GetToDoAsync(int id)
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            command.CommandText =
                @"
                SELECT todo_id, description, done
                FROM todo
                WHERE todo_id = @id";
            command.Parameters.AddWithValue("@id", id);
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.Read())
                {
                    var todoItem = new ToDoItem(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetBoolean(2)
                    );
                    _connection.Close();
                    //return Task.FromResult(todoItem);
                    return todoItem;
                }
            }
            return null!;
        }

        public async Task<IEnumerable<ToDoItem>> GetToDosAsync()
        {
            var toDoItems = new List<ToDoItem>();
            _connection.Open();
            var command = _connection.CreateCommand();
            command.CommandText =
                @"
               SELECT todo_id, description, done
                FROM todo";
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var todoItem = new ToDoItem(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetBoolean(2)
                    );
                    toDoItems.Add(todoItem);
                }
            }
            _connection.Close();
            return toDoItems;
        }

        public async Task<ToDoItem> UpdateToDoAsync(ToDoItem toDoItem)
        {
            _connection.Open();

            var command = _connection.CreateCommand();
            command.CommandText =
                @"
                UPDATE todo
                SET description = @description,
                    done = @done
                WHERE todo_id = @id";
            command.Parameters.AddWithValue("@description", toDoItem.Description);
            command.Parameters.AddWithValue("@done", toDoItem.IsComplete);
            command.Parameters.AddWithValue("@id", toDoItem.Id);
            await command.ExecuteNonQueryAsync();

            _connection.Close();
            return toDoItem;
        }
    }
}
