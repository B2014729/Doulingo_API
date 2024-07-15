using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Section;
using Doulingo_Api.Models;

namespace Doulingo_Api.Repositorys.InterfaceRepo
{
    public interface ISectionRepository : IRepository<Section>
    {
        Task<List<Section>> GetAllInfor();
        Task<Section?> GetSectionDetail(int id);
        Task<Section?> Update(int id, SectionRequestUpdate entity);
        Task<bool> SectionExist(int id);
    }
}