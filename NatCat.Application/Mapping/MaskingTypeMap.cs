using System;
using AutoMapper;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.MaskingType;

namespace NatCat.Application.Mapping
{
	public class MaskingTypeMap : Profile
	{
		public MaskingTypeMap()
		{
			CreateMap<MaskingType, MaskingTypeDto>();
		}
	}
}

