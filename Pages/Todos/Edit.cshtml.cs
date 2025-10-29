using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoRazor.Models;
using TodoRazor.Services;

namespace TodoRazor.Pages.Todos
{
    public class EditModel : PageModel
    {
        private readonly TodoService _svc;
        public EditModel(TodoService svc) => _svc = svc;

        [BindProperty]
        public TodoItem Item { get; set; } = new TodoItem();

        public IActionResult OnGet(Guid id)
        {
            var it = _svc.Get(id);
            if (it == null) return RedirectToPage("/Index");
            Item = it;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Item.Title))
            {
                ModelState.AddModelError("Item.Title", "Title is required.");
                return Page();
            }

            _svc.Update(Item);
            TempData["Message"] = "Task updated.";
            return RedirectToPage("/Index");
        }
    }
}
