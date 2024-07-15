using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.ChallengeChoose;
using Doulingo_Api.Models;

namespace Doulingo_Api.Repositorys.InterfaceRepo
{
    public interface IChallengeChooseRepository : IRepository<ChallengeChoose>
    {
        Task<ChallengeChoose?> Update(int id, ChallengeChooseRequestCreate challenge);
    }
}