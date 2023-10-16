using MediatR;
using NatCat.Application.Queries.BookClubs;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.QueryHandlers.BookClubs
{
    public class GetUserBookClubsHandler : IRequestHandler<GetUserBookClubs, IEnumerable<BookClubListDto>>
    {
        private readonly IRepository<BookClub, BookClubDetailDto, BookClubListDto> _bookClubRepository;

        public GetUserBookClubsHandler(IRepository<BookClub, BookClubDetailDto, BookClubListDto> bookClubRepository)
        {
            _bookClubRepository = bookClubRepository;
        }

        public async Task<IEnumerable<BookClubListDto>> Handle(GetUserBookClubs request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookClubRepository
                    .ListAllAsync(p => p.ApplicationUsers.Any(x => x.Id == request.UserId),
                    p => p.ApplicationUsers);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

