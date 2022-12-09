using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _db;
        public TodoController(TodoDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Todo>> Get()
        {
            return await _db.Todos.ToListAsync();
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _db.Todos.FindAsync(id);
            return todo == null ? NotFound() : Ok(todo);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Todo todo)
        {
            await _db.Todos.AddAsync(todo);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Todo todo)
        {
            if (id != todo.Id) return BadRequest();

            _db.Entry(todo).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _db.Todos.FindAsync(id);
            if(todo == null) return NotFound();

            _db.Entry(todo).State = EntityState.Deleted;
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
