using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Data;
using Doulingo_Api.Dtos.Section;
using Doulingo_Api.Models;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace Doulingo_Api.Repositorys
{
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        private readonly ApplicationContext _context;
        public SectionRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Section>> GetAllInfor()
        {
            return await _context.Sections.Include(l => l.Units).ThenInclude(l => l.Lessons).ToListAsync();
        }

        public async Task<Section?> GetSectionDetail(int id)
        {
            return await _context.Sections.Include(l => l.Units).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<bool> SectionExist(int id)
        {
            var unit = await _context.Sections.FirstOrDefaultAsync(u => u.Id == id);
            if (unit == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Section?> Update(int id, SectionRequestUpdate entity)
        {
            var section = await _context.Sections.FirstOrDefaultAsync(l => l.Id == id);
            if (section == null)
            {
                return null;
            }

            section.Title = entity.Title;
            section.Index = entity.Index;
            section.CourseId = entity.CourseId;
            section.At_Updated = DateTime.Now;

            await _context.SaveChangesAsync();
            return section;
        }
    }
}