using System;
using AutoMapper;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.Genre;

namespace NatCat.Application.Mapping
{
    public class GenreMap : Profile
    {
        public GenreMap()
        {
            CreateMap<Genre, GenreDetailDto>()
                .ForMember(m => m.KeyWordDetailDtos, m => m.MapFrom(x => x.KeyWords));
            CreateMap<Genre, GenreListDto>()
            	.ForMember(m => m.KeyWordDetailDtos, m => m.MapFrom(x => x.KeyWords));
        }
    }
}

