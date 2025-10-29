using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoRazor.Models;
using TodoRazor.Services;

namespace TodoRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TodoService _svc;
        public IndexModel(TodoService svc) => _svc = svc;

        public List<TodoItem> Items { get; set; } = new();

        public void OnGet()
        {
            Items = _svc.GetAll();
        }

        public IActionResult OnPostToggle(Guid id)
        {
            var item = _svc.Get(id);
            if (item != null)
            {
                item.IsDone = !item.IsDone;
                _svc.Update(item);
                TempData["Message"] = item.IsDone ? "Marked done." : "Marked not done.";
            }
            else
            {
                TempData["Message"] = "Task not found.";
            }
            return RedirectToPage();
        }
    }
}
