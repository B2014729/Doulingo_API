using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doulingo_Api.Data;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace Doulingo_Api.Repositorys
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        internal readonly DbSet<T> dbSet;

        public Repository(ApplicationContext context)
        {
            _context = context;
            dbSet = _context.Set<T>(); //_context.User()
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            IQueryable<T> query = dbSet;
            return await query.ToListAsync();
        }

        public async Task<T?> GetById(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> Create(T obj)
        {
            await dbSet.AddAsync(obj);
            return obj;
        }

        public T Remove(T obj)
        {
            dbSet.Remove(obj);
            return obj;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}