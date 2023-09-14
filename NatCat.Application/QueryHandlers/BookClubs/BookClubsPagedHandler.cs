using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Application.Queries.BookClubs;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.QueryHandlers.BookClubs
{
    public class BookClubsPagedHandler : IRequestHandler<BookClubsPaged, PagedResult<BookClubListDto>>
    {
        private readonly IRepository<BookClub, BookClubDetailDto, BookClubListDto> _bookClubRepository;

        public BookClubsPagedHandler(IRepository<BookClub, BookClubDetailDto, BookClubListDto> bookClubRepository)
        {
            _bookClubRepository = bookClubRepository;
        }

        public async Task<PagedResult<BookClubListDto>> Handle(BookClubsPaged request, CancellationToken cancellationToken)
        {
            QueryParameters<BookClub> qry = new()
            {
                wc = p => p.ApplicationUsers.Any(x => x.Id == request.UserId)
            };
            try
            {
                return await _bookClubRepository.PagedAsync(qry);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}