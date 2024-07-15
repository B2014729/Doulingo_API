using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Data;
using Doulingo_Api.Dtos.Unit;
using Doulingo_Api.Mappers;
using Doulingo_Api.Models;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace Doulingo_Api.Repositorys
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private readonly ApplicationContext _context;
        public UnitRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Unit>> GetAllInfor()
        {
            return await _context.Units
            .Include(u => u.Lessons)
            .ThenInclude(l => l.ChallengeArranges)
            .Include(u => u.Lessons)
            .ThenInclude(l => l.ChallengeChooses)
            .ToListAsync();
        }

        public async Task<Unit?> GetUnitDetail(int id)
        {
            return await _context.Units
            .Include(u => u.Lessons)
            .ThenInclude(l => l.ChallengeArranges)
            .Include(u => u.Lessons)
            .ThenInclude(l => l.ChallengeChooses)
            .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<bool> UnitExist(int id)
        {
            var unit = await _context.Units.FirstOrDefaultAsync(u => u.Id == id);
            if (unit == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Unit?> Update(int id, UnitRequestUpdate entity)
        {
            var unit = await _context.Units.FirstOrDefaultAsync(l => l.Id == id);
            if (unit == null)
            {
                return null;
            }

            unit.Title = entity.Title;
            unit.Description = entity.Description;
            unit.Index = entity.Index;
            unit.Point = entity.Point;
            unit.SectionId = entity.SectionId;
            unit.At_Updated = DateTime.Now;

            await _context.SaveChangesAsync();
            return unit;
        }
    }
}