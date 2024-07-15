using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Unit;
using Doulingo_Api.Models;

namespace Doulingo_Api.Repositorys.InterfaceRepo
{
    public interface IUnitRepository : IRepository<Unit>
    {

        Task<List<Unit>> GetAllInfor();
        Task<Unit?> GetUnitDetail(int id);
        Task<Unit?> Update(int id, UnitRequestUpdate unit);
        Task<bool> UnitExist(int id);
    }
}