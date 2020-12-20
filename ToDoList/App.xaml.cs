using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using ToDoList.Data;
using ToDoList.ViewModels;
using ToDoList.Views;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            //Get services
            TodoItemsContext context = serviceProvider.GetRequiredService<TodoItemsContext>();

            Window window = new MainView();
            window.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
            window.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {

            base.OnExit(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            //Inject services
            services.AddDbContext<TodoItemsContext>(opt =>
                opt.UseSqlite("Data source= .\\ToDoDb.db"));

            services.AddScoped<MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
