using Microsoft.AspNetCore.Mvc;
using ToDo.Shared;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDosAsync()
        {
            var toDos = await _toDoRepository.GetToDosAsync();
            return Ok(toDos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoAsync(int id)
        {
            var toDo = await _toDoRepository.GetToDoAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }
            return Ok(toDo);
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItem>> AddToDoAsync(ToDoItem toDoItem)
        {
            var toDo = await _toDoRepository.AddToDoAsync(toDoItem);
            return CreatedAtAction(nameof(GetToDoAsync), new { id = toDo.Id }, toDo);
        }

        [HttpPut]
        public async Task<ActionResult<ToDoItem>> UpdateToDoAsync(ToDoItem toDoItem)
        {
            var toDo = await _toDoRepository.UpdateToDoAsync(toDoItem);
            return Ok(toDo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteToDoAsync(int id)
        {
            await _toDoRepository.DeleteToDoAsync(id);
            return NoContent();
        }
    }
}
