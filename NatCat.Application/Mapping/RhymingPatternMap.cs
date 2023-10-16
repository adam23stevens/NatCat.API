using System;
using AutoMapper;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.RhymingPattern;

namespace NatCat.Application.Mapping
{
	public class RhymingPatternMap : Profile
	{
		public RhymingPatternMap()
		{
			CreateMap<RhymingPattern, RhymingPatternDto>();
		}
	}
}

