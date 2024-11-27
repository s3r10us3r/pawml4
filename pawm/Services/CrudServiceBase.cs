using pawm.Models;
using pawm.Services.Interfaces;
using System.Text.Json;

namespace pawm.Services
{
    public abstract class CrudServiceBase<T> : ICrudService<T> where T : Entity
    {
        private readonly string _path;
        private readonly SemaphoreSlim _fileSemaphore = new SemaphoreSlim(1, 1);

        protected CrudServiceBase(string path) 
        {
            _path = path;
        }

        public virtual async Task<T> Create(T entity)
        {
            var list = await ReadList();
            list.Add(entity);
            await SaveList(list);
            return entity;
        }

        public virtual async Task Delete(string id)
        {
            var list = await ReadList();
            list.RemoveAll(t => t.Id == id);
            await SaveList(list);
        }

        public virtual async Task<List<T>> ReadAll()
        {
            List<T> result = [];
            result = await ReadList();
            return result;
        }

        public virtual async Task<T?> ReadOne(string Id)
        {
            T? result = null;
            result = (await ReadList()).Find(t => t.Id == Id);
            return result;
        }

        public virtual async Task<T> Update(T entity)
        {
            var list = await ReadList();
            var index = list.FindIndex(t => t.Id == entity.Id);
            if (index == -1)
                throw new Exception("Id not found");
            list[index] = entity;
            await SaveList(list);
            return entity;
        }

        protected async Task<List<T>> ReadList()
        {
            await _fileSemaphore.WaitAsync();
            try
            {
                if (File.Exists(_path))
                {
                    using var fileStream = File.OpenRead(_path);
                    var result = await JsonSerializer.DeserializeAsync<List<T>>(fileStream);
                    return result ?? [];
                }
                return [];
            }
            finally
            {
                _fileSemaphore.Release();
            }
        }

        protected async Task SaveList(List<T> list)
        {
            await _fileSemaphore.WaitAsync();
            try
            {
                using var fileStream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.None);
                await JsonSerializer.SerializeAsync(fileStream, list);
            }
            finally
            {
                _fileSemaphore.Release();
            }
        }
    }
}
