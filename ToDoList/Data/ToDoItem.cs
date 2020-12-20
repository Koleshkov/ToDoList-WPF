using DevExpress.Mvvm;
using System;

namespace ToDoList.Data
{
    public class ToDoItem : ViewModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
