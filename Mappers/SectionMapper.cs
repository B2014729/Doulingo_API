using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Section;
using Doulingo_Api.Models;

namespace Doulingo_Api.Mappers
{
    public static class SectionMapper
    {
        public static SectionDto ToSectionDto(this Section section)
        {
            return new SectionDto
            {
                Id = section.Id,
                Title = section.Title,
                Index = section.Index,
                CourseId = section.CourseId,
                At_Created = section.At_Created,
                At_Updated = section.At_Updated,
                Units = section.Units.Select(u => u.ToUnitDto()).ToList(),
            };
        }

        public static Section ToSectionFromSectionCreateDto(this SectionRequestCreate section, Course course)
        {
            return new Section()
            {
                Title = section.Title,
                Index = section.Index,
                CourseId = course.Id,
                Course = course,
            };
        }
    }
}