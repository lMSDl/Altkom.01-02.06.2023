using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class CrudService<T> : ICrudService<T> where T : Entity
    {
        private ICollection<T> _entities;

        public CrudService(EntityFaker<T> faker, int min, int max)
        {
            _entities = faker.Generate(new Random().Next(min, max));

        }

        public Task<T> CreateAsync(T entity)
        {
            entity.Id = _entities.Max(x => x.Id) + 1;
            _entities.Add(entity);

            return Task.FromResult(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await ReadAsync(id);
            if(entity != null)
                _entities.Remove(entity);
        }

        public Task<T?> ReadAsync(int id)
        {
            return Task.FromResult(_entities.SingleOrDefault(x => x.Id == id));

        }

        public Task<IEnumerable<T>> ReadAsync()
        {
            return Task.FromResult(_entities.ToList().AsEnumerable());
        }

        public async Task UpdateAsync(int id, T entity)
        {
            if(_entities.Any(x => x.Id == id))
            {
                await DeleteAsync(id);
                entity.Id = id;
                _entities.Add(entity);
            }
        }
    }
}