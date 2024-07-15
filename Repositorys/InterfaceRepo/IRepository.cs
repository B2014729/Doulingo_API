using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Doulingo_Api.Repositorys.InterfaceRepo
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetById(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAll();
        Task<T> Create(T obj);
        T Remove(T obj);
        Task Save();
    }
}