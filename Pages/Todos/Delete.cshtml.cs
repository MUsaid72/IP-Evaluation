using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoRazor.Models;
using TodoRazor.Services;

namespace TodoRazor.Pages.Todos
{
    public class DeleteModel : PageModel
    {
        private readonly TodoService _svc;
        public DeleteModel(TodoService svc) => _svc = svc;

        [BindProperty]
        public TodoItem? Item { get; set; }

        public IActionResult OnGet(Guid id)
        {
            Item = _svc.Get(id);
            return Page();
        }

        public IActionResult OnPost(Guid id)
        {
            _svc.Delete(id);
            TempData["Message"] = "Task deleted.";
            return RedirectToPage("/Index");
        }
    }
}
