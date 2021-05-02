using System.Collections.Generic;

namespace bugLog.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> Get();
        T Get(int id);
        T Insert(T item);
        T Delete(int id);
        T Update(T item);
        void Save();
    }
}
