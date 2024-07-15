using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Course;
using Doulingo_Api.Models;

namespace Doulingo_Api.Mappers
{
    public static class CourseMapper
    {
        public static CourseDto ToCourseDto(this Course course)
        {
            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                ImageSrc = course.ImageSrc,
                Sections = course.Sections.Select(s => s.ToSectionDto()).ToList(),
                At_Created = course.At_Created,
                At_Updated = course.At_Updated,
            };
        }

        public static Course ToCourseFromCourseCreateDto(this CourseRequestCreate course)
        {
            return new Course()
            {
                Title = course.Title,
                ImageSrc = course.ImageSrc,
            };
        }
    }
}