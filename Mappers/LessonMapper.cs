using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Lesson;
using Doulingo_Api.Models;

namespace Doulingo_Api.Mappers
{
    public static class LessionMapper
    {
        public static LessonDto ToLessonDto(this Lesson lesson)
        {
            return new LessonDto()
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Index = lesson.Index,
                Point = lesson.Point,
                At_Created = lesson.At_Created,
                At_Updated = lesson.At_Updated,
                UnitId = lesson.UnitId,
                ChallengChooses = lesson.ChallengeChooses.Select(c => c.ToChallengeChooseDto()).ToList(),
                ChallengeArranges = lesson.ChallengeArranges.Select(c => c.ToChallengeArrangeDto()).ToList(),
            };
        }
        public static Lesson ToLessonFromLessonCreateDto(this LessonRequestCreate lesson, Unit unit)
        {
            return new Lesson()
            {
                Title = lesson.Title,
                Index = lesson.Index,
                Point = lesson.Point,
                UnitId = unit.Id,
                Unit = unit,
                At_Created = DateTime.Now,
            };
        }
    }
}