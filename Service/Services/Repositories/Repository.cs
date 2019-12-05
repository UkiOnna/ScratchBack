using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ScratchContext _context;

        public Repository(ScratchContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            if (entity != null)
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public T FindById(int id)
        {
            return _context.Set<T>().FirstOrDefault(i => i.Id == id);
        }

        public void Remove(int id)
        {
            var entity = _context.Set<T>().FirstOrDefault(s => s.Id == id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<T> AsQueryable()
        {
            return _context.Set<T>().AsQueryable<T>();
        }

        public IQueryable<T> AsNoTracking()
        {
            return _context.Set<T>().AsNoTracking();
        }
    }
}
