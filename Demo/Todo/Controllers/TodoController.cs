using Microsoft.AspNetCore.Mvc;
using Todo.Models;

namespace Todo.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    // IN-MEMORY TODO LIST (WOULD BE REPLACED WITH DATABASE IN PRODUCTION)
    private static readonly List<TodoItem> _todos = new();

    private readonly ILogger<TodoController> _logger;

    public TodoController(ILogger<TodoController> logger)
    {
        _logger = logger;
    }

    // GET ALL TODOS
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetAll()
    {
        _logger.LogInformation("Getting all todos");
        return Ok(_todos);
    }

    // GET SINGLE TODO BY ID
    [HttpGet("{id:guid}")]
    public ActionResult<TodoItem> GetById(Guid id)
    {
        _logger.LogInformation("Getting todo with id {Id}", id);

        var todo = _todos.FirstOrDefault(t => t.Id == id);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    // CREATE NEW TODO
    [HttpPost]
    public ActionResult<TodoItem> Create(TodoItem todo)
    {
        _logger.LogInformation("Creating new todo");

        todo.Id = Guid.NewGuid();
        todo.CreatedAt = DateTime.UtcNow;

        _todos.Add(todo);

        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

    // UPDATE EXISTING TODO
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, TodoItem todo)
    {
        _logger.LogInformation("Updating todo with id {Id}", id);

        var index = _todos.FindIndex(t => t.Id == id);

        if (index == -1)
        {
            return NotFound();
        }

        // PRESERVE CREATION TIME AND ID
        todo.Id = id;
        todo.CreatedAt = _todos[index].CreatedAt;

        // UPDATE COMPLETION TIMESTAMP IF NEWLY COMPLETED
        if (todo.IsCompleted && !_todos[index].IsCompleted)
        {
            todo.CompletedAt = DateTime.UtcNow;
        }
        else if (!todo.IsCompleted)
        {
            todo.CompletedAt = null;
        }

        _todos[index] = todo;

        return NoContent();
    }

    // DELETE TODO
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        _logger.LogInformation("Deleting todo with id {Id}", id);

        var index = _todos.FindIndex(t => t.Id == id);

        if (index == -1)
        {
            return NotFound();
        }

        _todos.RemoveAt(index);

        return NoContent();
    }

    // TOGGLE TODO COMPLETION STATUS
    [HttpPatch("{id:guid}/toggle")]
    public IActionResult ToggleCompletion(Guid id)
    {
        _logger.LogInformation("Toggling completion status for todo with id {Id}", id);

        var todo = _todos.FirstOrDefault(t => t.Id == id);

        if (todo == null)
        {
            return NotFound();
        }

        todo.IsCompleted = !todo.IsCompleted;

        if (todo.IsCompleted)
        {
            todo.CompletedAt = DateTime.UtcNow;
        }
        else
        {
            todo.CompletedAt = null;
        }

        return NoContent();
    }
}
