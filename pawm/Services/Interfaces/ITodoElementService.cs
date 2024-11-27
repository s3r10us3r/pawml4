using pawm.Models;

namespace pawm.Services.Interfaces
{
    public interface ITodoElementService : ICrudService<TodoElement>
    {
        public Task<List<TodoElement>> GetAllByListId(string listId);
    }
}
