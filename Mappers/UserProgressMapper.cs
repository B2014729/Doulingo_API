using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.UserProgress;
using Doulingo_Api.Models;

namespace Doulingo_Api.Mappers
{
    public static class UserProgressMapper
    {
        public static UserProgressDto ToUserProgressDto(this UserProgress userProgress)
        {
            return new UserProgressDto()
            {
                Id = userProgress.Id,
                Point = userProgress.Point,
                UserId = userProgress.UserId,
                CourseId = userProgress.CourseId,
                At_Created = userProgress.At_Created,
                At_Updated = userProgress.At_Updated,
            };
        }

        public static UserProgress ToUserProgressFromUserProgressCreateDto(this UserProgressRequestCreate userProgress, User user, Course course)
        {
            return new UserProgress()
            {
                Point = userProgress.Point,
                UserId = userProgress.UserId,
                CourseId = userProgress.CourseId,
                User = user,
                Course = course,
            };
        }
    }
}