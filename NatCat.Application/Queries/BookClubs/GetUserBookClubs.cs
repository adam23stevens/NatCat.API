using System;
using MediatR;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.Queries.BookClubs
{
    public class GetUserBookClubs : IRequest<IEnumerable<BookClubListDto>>
    {
        public string UserId { get; set; }
        public GetUserBookClubs(string userId)
        {
            UserId = userId;
        }
    }
}

