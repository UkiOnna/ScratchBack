using Domain.Data;
using Domain.Entities;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Services.Repositories
{
    public class RepositoryService
    {
        private readonly ScratchContext _context;
        public RepositoryService(ScratchContext context)
        {
            _context = context;
        }
        public IRepository<T> GetRepository<T>() where T : Entity
        {
            return new Repository<T>(_context);
        }
    }
}
