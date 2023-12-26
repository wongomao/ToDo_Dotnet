using Microsoft.Data.Sqlite;
using System.Data.Common;
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

        public Task<ToDoItem> AddToDoAsync(ToDoItem toDoItem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteToDoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ToDoItem> GetToDoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ToDoItem>> GetToDosAsync()
        {
            var toDoItems = new List<ToDoItem>();
            _connection.Open();
            var command = _connection.CreateCommand();
            command.CommandText =
                @"
               SELECT todo_id, description, done
                FROM todo";
            using (var reader = command.ExecuteReader())
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
            return Task.FromResult<IEnumerable<ToDoItem>>(toDoItems);
        }

        public Task<ToDoItem> UpdateToDoAsync(ToDoItem toDoItem)
        {
            throw new NotImplementedException();
        }
    }
}
