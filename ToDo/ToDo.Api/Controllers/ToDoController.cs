using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
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
            if (toDoRepository == null)
            {
                throw new ArgumentNullException(nameof(toDoRepository));
            }
            _toDoRepository = toDoRepository;
        }

        // GET: api/ToDo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDos()
        {
            var toDos = await _toDoRepository.GetToDosAsync();
            return Ok(toDos);
        }

        // GET: api/ToDo/5
        [HttpGet("{id}", Name =nameof(GetToDo))]
        public async Task<ActionResult<ToDoItem>> GetToDo([FromRoute]int id)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var toDo = await _toDoRepository.GetToDoAsync(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (toDo == null)
            {
                return NotFound();
            }
            return Ok(toDo);
        }

        // POST: api/ToDo
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> AddToDo([FromBody]ToDoItem toDoItem)
        {
            var toDo = await _toDoRepository.AddToDoAsync(toDoItem);
            return CreatedAtAction(nameof(GetToDo), new { id = toDo.Id }, toDo);
        }

        // PUT: api/ToDo
        [HttpPut]
        public async Task<ActionResult<ToDoItem>> UpdateToDo([FromBody]ToDoItem toDoItem)
        {
            var toDo = await _toDoRepository.UpdateToDoAsync(toDoItem);
            return Ok(toDo);
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteToDo([FromRoute]int id)
        {
            await _toDoRepository.DeleteToDoAsync(id);
            return NoContent();
        }
    }
}
