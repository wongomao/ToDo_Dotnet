namespace ToDo.Shared
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsComplete { get; set; } = false;

        public ToDoItem()
        {
            Id = 0;
            Description = string.Empty;
            IsComplete = false;
        }

        public ToDoItem(string description)
        {
            Id = 0;
            Description = description;
            IsComplete = false;
        }

        public ToDoItem(int id, string description, bool isComplete)
        {
            Id = id;
            Description = description;
            IsComplete = isComplete;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Description: {Description}, IsComplete: {IsComplete}";
        }
    }
}
