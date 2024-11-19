using Reservas.Dtos;
using Reservas.Models;
using System.Collections.Generic;
using System.Linq;

namespace Reservas.Mappers
{
	public static class UserMapper
	{
		/// <summary>
		/// Converte uma entidade User para um DTO UserDto.
		/// </summary>
		public static UserDto ToDto(User user)
		{
			if (user == null) return null;

			return new UserDto
			{
				Name = user.Name,
				password = user.password
			};
		}

		/// <summary>
		/// Converte um DTO UserDto para uma entidade User.
		/// </summary>
		public static User ToEntity(UserDto userDto)
		{
			if (userDto == null) return null;

			return new User
			{
				Name = userDto.Name,
				password = userDto.password
			};
		}

		/// <summary>
		/// Converte uma lista de entidades User para uma lista de DTOs UserDto.
		/// </summary>
		public static List<UserDto> ToDtoList(List<User> users)
		{
			// Verifica se a lista é nula ou vazia antes de tentar realizar a conversão
			return users?.Select(ToDto).ToList();
		}

		/// <summary>
		/// Converte uma lista de DTOs UserDto para uma lista de entidades User.
		/// </summary>
		public static List<User> ToEntityList(List<UserDto> userDtos)
		{
			// Verifica se a lista é nula ou vazia antes de tentar realizar a conversão
			return userDtos?.Select(ToEntity).ToList();
		}
	}
}
