using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.DAL.Entity;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.Queries.BookClubs
{
    public class BookClubsPaged : IRequest<PagedResult<BookClubListDto>>
    {
        public string UserId {get;}
        public BookClubsPaged(string userId)
        {
            UserId = userId;
        }
    }
}