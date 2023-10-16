using AutoMapper;
using NatCat.Application.Commands.BookClubs;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.Mapping
{
    public class BookClubMap : Profile
    {
        public BookClubMap()
        {
            CreateMap<BookClub, BookClubDetailDto>()
                   .ForMember(x => x.UserDtos, x => x.MapFrom(y => y.ApplicationUsers));
            CreateMap<BookClub, BookClubListDto>()
                   .ForMember(x => x.UserDtos, x => x.MapFrom(y => y.ApplicationUsers));
            CreateMap<AddBookClub, BookClub>();
            CreateMap<BookClubJoinRequest, BookClubJoinRequestDetailDto>();
            CreateMap<BookClubJoinRequest, BookClubJoinRequestListDto>();
        }
    }
}