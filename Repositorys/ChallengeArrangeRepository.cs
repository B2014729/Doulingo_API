using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Data;
using Doulingo_Api.Dtos.ChallengeArrange;
using Doulingo_Api.Models;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace Doulingo_Api.Repositorys
{
    public class ChallengeArrangeRepository : Repository<ChallengeArrange>, IChallengeArrangeRepository
    {
        private readonly ApplicationContext _context;
        public ChallengeArrangeRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ChallengeArrange?> Update(int id, ChallengeArrangeRequestCreate entity)
        {
            var challenge = await _context.ChallengeArranges.FirstOrDefaultAsync(l => l.Id == id);
            if (challenge == null)
            {
                return null;
            }

            challenge.Question = entity.Question;
            challenge.Options = entity.Options;
            challenge.Answer = entity.Answer;
            challenge.LessonId = entity.LessonId;
            challenge.At_Updated = DateTime.Now;

            await _context.SaveChangesAsync();
            return challenge;
        }
    }
}