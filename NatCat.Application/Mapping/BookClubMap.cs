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
                   .ForMember(x => x.ApplicationUserProfileNames, x => x.MapFrom(y => y.ApplicationUsers.Select(x => x.ProfileName)));
            CreateMap<BookClub, BookClubListDto>();
            CreateMap<AddBookClub, BookClub>();
        }
    }
}