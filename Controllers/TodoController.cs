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
        
    }
}