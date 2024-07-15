using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.ChallengeArrange;
using Doulingo_Api.Dtos.ChallengeChoose;
using Doulingo_Api.Models;
namespace Doulingo_Api.Mappers
{
    public static class ChallengeMapper
    {
        public static ChallengeChooseDto ToChallengeChooseDto(this ChallengeChoose challenge)
        {
            return new ChallengeChooseDto()
            {
                Id = challenge.Id,
                Question = challenge.Question,
                Options_A = challenge.Options_A,
                Options_B = challenge.Options_B,
                Options_C = challenge.Options_C,
                Options_D = challenge.Options_D,
                Answer = challenge.Answer,
                LessonId = challenge.LessonId,
                At_Created = challenge.At_Created,
                At_Updated = challenge.At_Updated
            };
        }

        public static ChallengeChoose ToChallengeChooseFromChallengeChooseRequestCreate(this ChallengeChooseRequestCreate challenge, Lesson lesson)
        {
            return new ChallengeChoose()
            {
                Question = challenge.Question,
                Options_A = challenge.Options_A,
                Options_B = challenge.Options_B,
                Options_C = challenge.Options_C,
                Options_D = challenge.Options_D,
                Answer = challenge.Answer,
                LessonId = challenge.LessonId,
                Lesson = lesson
            };
        }

        public static ChallengeArrangeDto ToChallengeArrangeDto(this ChallengeArrange challenge)
        {
            return new ChallengeArrangeDto()
            {
                Id = challenge.Id,
                Question = challenge.Question,
                Options = challenge.Options,
                Answer = challenge.Answer,
                LessonId = challenge.LessonId,
                SoundSrc = challenge.SoundSrc,
                At_Created = challenge.At_Created,
                At_Updated = challenge.At_Updated
            };
        }

        public static ChallengeArrange ToChallengeArrangeFromChallengeArrangeRequestCreate(this ChallengeArrangeRequestCreate challenge, Lesson lesson)
        {
            return new ChallengeArrange()
            {
                Question = challenge.Question,
                Options = challenge.Options,
                Answer = challenge.Answer,
                LessonId = challenge.LessonId,
                Lesson = lesson,
                SoundSrc = challenge.SoundSrc,
            };
        }
    }
}