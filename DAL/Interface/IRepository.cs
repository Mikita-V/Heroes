using System.Collections.Generic;

namespace DAL.Interface
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
