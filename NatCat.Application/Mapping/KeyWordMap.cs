using System;
using AutoMapper;
using NatCat.DAL.Entity;
using NatCat.DAL.Web.Request.Base.NatCat.API.Model.Request.KeyWords;
using NatCat.Model.Dto.KeyWord;

namespace NatCat.Application.Mapping
{
	public class KeyWordMap : Profile
	{
		public KeyWordMap()
		{
			CreateMap<KeyWord, KeyWordDetailDto>();

			CreateMap<AddKeyWordReq, KeyWord>()
				.ForMember(m => m.GenreId, m => m.MapFrom(x => x.ParentId));

		}
	}
}

