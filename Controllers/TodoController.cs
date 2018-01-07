using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context){
            _context = context;

            if (_context.TodoItem.Count() == 0){
                _context.TodoItem.Add(new TodoItem{Name = "Item 1"});
                _context.SaveChanges();
            }
        }
        
        [HttpGet]
        public IEnumerable<TodoItem> GetAll(){
            return _context.TodoItem.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id){
            var item = _context.TodoItem.FirstOrDefault(t => t.Id == id);
            if (item == null){
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item){
            if(item ==null){
                return BadRequest();
            }

            _context.TodoItem.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new {id = item.Id}, item);
        }
    }
}