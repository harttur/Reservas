using Reservas.Dtos;
using Reservas.Models;

namespace Reservas.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user) 
        {
            if (user == null) return null;

            return new UserDto
            {
                Name = user.Name,
                password = user.password
            };

        }

        public static User ToEntity(UserDto userDto)
        {
            if (userDto == null) return null;

            return new User
            {
                Name = userDto.Name,
                password = userDto.password
            };

        }

        public static List<UserDto> ToDtoList(List<User> user)

        {
            return user?.Select(ToDto).ToList();
        }

        public static List<User> ToEntity(List<UserDto> userDto)

        {
            return userDto?.Select(ToEntity).ToList();
        }
    }
}
