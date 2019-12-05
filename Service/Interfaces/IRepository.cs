using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);

        void Remove(int id);

        void Update(T entity);

        IEnumerable<T> GetAll();

        T FindById(int id);
        IQueryable<T> AsQueryable();
        IQueryable<T> AsNoTracking();
    }
}
