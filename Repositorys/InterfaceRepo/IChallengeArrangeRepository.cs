using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.ChallengeArrange;
using Doulingo_Api.Models;

namespace Doulingo_Api.Repositorys.InterfaceRepo
{
    public interface IChallengeArrangeRepository : IRepository<ChallengeArrange>
    {
        Task<ChallengeArrange?> Update(int id, ChallengeArrangeRequestCreate challenge);
    }
}