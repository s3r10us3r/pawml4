using pawm.Models;
using pawm.Services.Interfaces;

namespace pawm.Services
{
    public class TodoListService : CrudServiceBase<TodoList>, ITodoListService
    {
        public TodoListService() : base(Path.Combine(FileSystem.AppDataDirectory, "lists.json"))
        {
        }
    }
}
