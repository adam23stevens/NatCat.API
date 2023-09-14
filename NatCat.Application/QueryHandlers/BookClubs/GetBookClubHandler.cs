using System;
using MediatR;
using NatCat.Application.Queries.BookClubs;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.QueryHandlers.BookClubs
{
	public class GetBookClubHandler : IRequestHandler<GetBookClub, BookClubDetailDto>
	{
        private IRepository<BookClub, BookClubDetailDto, BookClubListDto> _bookClubRepository;
		public GetBookClubHandler(IRepository<BookClub, BookClubDetailDto, BookClubListDto> bookClubRepository)
		{
            _bookClubRepository = bookClubRepository;
		}

        public async Task<BookClubDetailDto> Handle(GetBookClub request, CancellationToken cancellationToken)
        {
            var bookClub = await _bookClubRepository.GetAsync(request.BookClubId);

            return bookClub;
        }
    }
}

