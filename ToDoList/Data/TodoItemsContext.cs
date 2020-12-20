using Microsoft.EntityFrameworkCore;

namespace ToDoList.Data
{
    public class TodoItemsContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public TodoItemsContext(DbContextOptions<TodoItemsContext> opt) : base(opt)
        {
            Database.EnsureCreated();
        }
    }
}
