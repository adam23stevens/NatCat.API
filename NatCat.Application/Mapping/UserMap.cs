using System;
using AutoMapper;
using NatCat.DAL.Entity;
using NatCat.Model.Auth;

namespace NatCat.Application.Mapping
{
	public class UserMap : Profile
	{
		public UserMap()
		{
			CreateMap<ApplicationUser, UserDto>();
		}
	}
}

