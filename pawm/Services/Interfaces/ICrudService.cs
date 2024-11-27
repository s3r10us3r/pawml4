using pawm.Models;

namespace pawm.Services.Interfaces
{
    public interface ICrudService<T> where T : Entity
    {
        public Task<T> Create(T entity);
        public Task<T> Update(T entity);
        public Task<T?> ReadOne(string Id);
        public Task<List<T>> ReadAll();
        public Task Delete(string Id);
    }
}
