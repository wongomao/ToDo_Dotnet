using ToDo.Shared;

namespace ToDo.Api
{
    public interface IToDoRepository
    {
        public Task<IEnumerable<ToDoItem>> GetToDosAsync();
        public Task<ToDoItem> GetToDoAsync(int id);
        public Task<ToDoItem> AddToDoAsync(ToDoItem toDoItem);
        public Task<ToDoItem> UpdateToDoAsync(ToDoItem toDoItem);
        public Task DeleteToDoAsync(int id);
    }
}
