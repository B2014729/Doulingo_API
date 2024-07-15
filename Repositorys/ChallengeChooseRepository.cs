using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Data;
using Doulingo_Api.Dtos.ChallengeChoose;
using Doulingo_Api.Models;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace Doulingo_Api.Repositorys
{
    public class ChallengeChooseRepository : Repository<ChallengeChoose>, IChallengeChooseRepository
    {
        private readonly ApplicationContext _context;
        public ChallengeChooseRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ChallengeChoose?> Update(int id, ChallengeChooseRequestCreate entity)
        {
            var challenge = await _context.ChallengeChooses.FirstOrDefaultAsync(l => l.Id == id);
            if (challenge == null)
            {
                return null;
            }

            challenge.Question = entity.Question;
            challenge.Options_A = entity.Options_A;
            challenge.Options_B = entity.Options_B;
            challenge.Options_C = entity.Options_C;
            challenge.Options_D = entity.Options_D;
            challenge.Answer = entity.Answer;
            challenge.LessonId = entity.LessonId;
            challenge.At_Updated = DateTime.Now;

            await _context.SaveChangesAsync();
            return challenge;
        }
    }
}