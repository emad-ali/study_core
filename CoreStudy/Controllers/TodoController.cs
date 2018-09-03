using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreStudy.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreStudy.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }
        public async Task< IActionResult> Index()
        {
            var items = await _todoItemService.GetIncompleteItemAsync();
            var vm = new TodoViewModel
            {
                Items = items
            };
            return View(vm);
        }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem item)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");
            var result = await _todoItemService.AddItemAsync(item);
            if (!result)
            {
                return BadRequest("Faild");
            }
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }
            var successful = await _todoItemService.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }
            return RedirectToAction("Index");
        }
    }
}