using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Data;

namespace ToDoList.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly TodoItemsContext context;
        private ToDoItem selectedToDoItem;
        public int PageNumber { get; set; }
        public ToDoItem NewToDoItem { get; set; }
        public ToDoItem SelectedToDoItem 
        {
            get
            {   
                return selectedToDoItem;
            }
            set
            {
                if (selectedToDoItem!=null)
                {
                    Task.Run(async () =>
                    {
                        context.ToDoItems.Update(selectedToDoItem);
                        await context.SaveChangesAsync();
                    });
                }
                selectedToDoItem = value;
            }
        }
        public List<ToDoItem> Items { get; set; }

        public MainViewModel(TodoItemsContext context)
        {
            this.context = context;

            PageNumber = 0;

            Task.Run(() => LoadItems());
        }

        private async Task LoadItems()
        {
            Items = await context.ToDoItems.ToListAsync();
            NewToDoItem = new ToDoItem();

            SelectedToDoItem = Items.FirstOrDefault(x => x != null);
            PageNumber = (SelectedToDoItem == null) && (Items.Count==0) ? 1 : 0;
        }

        public ICommand AddNewItem
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    if (NewToDoItem!=null)
                    {
                        context.ToDoItems.Add(NewToDoItem);
                        await context.SaveChangesAsync();
                    }
                    await LoadItems();
                });
            }
        }

        public ICommand DeleteItem
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    if (SelectedToDoItem != null)
                    {
                        context.ToDoItems.Remove(SelectedToDoItem);
                        await context.SaveChangesAsync();
                    }
                    await LoadItems();
                });
            }
        }

        public ICommand CreateNewItem
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    PageNumber = 1;
                });
            }
        }
    }
}
