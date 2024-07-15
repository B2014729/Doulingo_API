using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.User;
using Doulingo_Api.Models;

namespace Doulingo_Api.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                AvatarUrl = user.AvatarUrl,
                At_Created = user.At_Created,
                At_Updated = user.At_Updated,
            };
        }

        public static User ToUserFromUserCreateDto(this UserRequestCreate user)
        {
            return new User()
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                AvatarUrl = "",
            };
        }

        public static User ToUserFromUserUpdateDto(this UserRequestUpdate user)
        {
            return new User()
            {
                Username = user.Username,
                Email = user.Email,
                Password = "",
                AvatarUrl = "",
            };
        }
    }

}