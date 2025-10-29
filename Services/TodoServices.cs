using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TodoRazor.Models;

namespace TodoRazor.Services
{
    public class TodoService
    {
        private readonly string _folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoRazor");
        private readonly string _file;
        private List<TodoItem> _items;

        public TodoService()
        {
            _file = Path.Combine(_folder, "todos.json");
            _items = Load();
            if (!_items.Any())
            {
                _items = new List<TodoItem>
                {
                    new TodoItem { Title = "Study Physics - Chapter 1", Notes = "Read theory + solved examples", DueDate = DateTime.Today.AddDays(2) },
                    new TodoItem { Title = "Finish math exercises", Notes = "Problems 1-20", DueDate = DateTime.Today.AddDays(1) },
                    new TodoItem { Title = "Chemistry practical prep", Notes = "Prepare reagents", DueDate = DateTime.Today.AddDays(3) }
                };
                Save();
            }
        }

        private List<TodoItem> Load()
        {
            try
            {
                if (!File.Exists(_file)) return new List<TodoItem>();
                var json = File.ReadAllText(_file);
                return JsonSerializer.Deserialize<List<TodoItem>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<TodoItem>();
            }
            catch { return new List<TodoItem>(); }
        }

        private void Save()
        {
            try
            {
                Directory.CreateDirectory(_folder);
                var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_file, json);
            }
            catch { }
        }

        public List<TodoItem> GetAll() => _items.OrderBy(x => x.IsDone).ThenByDescending(x => x.AddedOn).ToList();
        public TodoItem? Get(Guid id) => _items.FirstOrDefault(x => x.Id == id);
        public void Add(TodoItem t) { _items.Add(t); Save(); }
        public void Update(TodoItem t)
        {
            var ex = Get(t.Id);
            if (ex == null) return;
            ex.Title = t.Title;
            ex.Notes = t.Notes;
            ex.IsDone = t.IsDone;
            ex.DueDate = t.DueDate;
            Save();
        }
        public void Delete(Guid id) { _items.RemoveAll(x => x.Id == id); Save(); }
    }
}
