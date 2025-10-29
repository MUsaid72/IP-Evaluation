using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoRazor.Models;
using TodoRazor.Services;

namespace TodoRazor.Pages.Todos
{
    public class CreateModel : PageModel
    {
        private readonly TodoService _svc;
        public CreateModel(TodoService svc) => _svc = svc;

        [BindProperty]
        public TodoItem Item { get; set; } = new TodoItem();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Item.Title))
            {
                ModelState.AddModelError("Item.Title", "Title is required.");
                return Page();
            }
            _svc.Add(Item);
            TempData["Message"] = "Task added.";
            return RedirectToPage("/Index");
        }
    }
}
