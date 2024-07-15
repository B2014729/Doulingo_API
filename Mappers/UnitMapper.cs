using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Unit;
using Doulingo_Api.Models;

namespace Doulingo_Api.Mappers
{
    public static class UnitMapper
    {
        public static UnitDto ToUnitDto(this Unit unit)
        {
            return new UnitDto()
            {
                Id = unit.Id,
                Title = unit.Title,
                Description = unit.Description,
                Index = unit.Index,
                Point = unit.Point,
                At_Created = unit.At_Created,
                At_Updated = unit.At_Updated,
                SectionId = unit.SectionId,
                Lessons = unit.Lessons.Select(l => l.ToLessonDto()).ToList(),
            };
        }

        public static Unit ToUnitFromUnitCreateDto(this UnitRequestCreate unit, Section section)
        {
            return new Unit()
            {
                Title = unit.Title,
                Description = unit.Description,
                Index = unit.Index,
                Point = unit.Point,
                SectionId = section.Id,
                Section = section,
            };
        }
    }
}