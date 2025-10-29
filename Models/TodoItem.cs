using System;

namespace TodoRazor.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";
        public string? Notes { get; set; }
        public bool IsDone { get; set; } = false;
        public DateTime? DueDate { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
