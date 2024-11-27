using pawm.Models;
using pawm.Services.Interfaces;
using System.Text.Json;

namespace pawm.Services
{
    public class TodoElementService : CrudServiceBase<TodoElement>, ITodoElementService
    {
        public TodoElementService() : base(Path.Combine(FileSystem.AppDataDirectory, "elements.json"))
        {
        }

        public async Task<List<TodoElement>> GetAllByListId(string listId)
        {
            var list = await ReadAll();
            return list.Where(t => t.ListId == listId).ToList();
        }
    }
}
