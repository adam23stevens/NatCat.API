using AutoMapper;
using MediatR;
using NatCat.Application.Commands.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.BookClub;
using NatCat.Model.Dto.Genre;
using NatCat.Model.Dto.KeyWord;
using NatCat.Model.Dto.Story;
using NatCat.Model.Enum;
using NatCat.Model.Web.Story;

namespace NatCat.Application.CommandHandlers.Stories
{
    public class AddNewStoryHandler : IRequestHandler<AddStory>
    {
        private IMapper _mapper;
        private IRepository<BookClub, BookClubDetailDto, BookClubListDto> _bookClubRepository;
        private IRepository<Genre, GenreDetailDto, GenreListDto> _genreRepository;
        private IRepository<Story, StoryDetailDto, StoryListDto> _storyRepository;
        public AddNewStoryHandler(IMapper mapper,
        IRepository<BookClub, BookClubDetailDto, BookClubListDto> bookClubRepository,
        IRepository<Genre, GenreDetailDto, GenreListDto> genreRepository,
        IRepository<Story, StoryDetailDto, StoryListDto> storyRepository)
        {
            _mapper = mapper;
            _bookClubRepository = bookClubRepository;
            _genreRepository = genreRepository;
            _storyRepository = storyRepository;
        }

        public async Task<Unit> Handle(AddStory request, CancellationToken cancellationToken)
        {
            var req = request.AddStoryReq;

            if (req.BookClubId != null)
            {
                var bookClub = await _bookClubRepository.GetEntityAsync(req.BookClubId);
                if (bookClub == null)
                {
                    throw new Exception("BookClub could not be found");
                }
            }

            var story = _mapper.Map<AddStoryReq, Story>(req);

            story.DateCreated = DateTime.Now;
            story.IsBeingWritten = true;
            story.IsVisibleOnLibrary = false;
            story.StoryTypeId = req.StoryTypeId;

            story.AuthorApplicationUserId = request.LoggedInUserId;

            if (req.BookClubId != null)
            {
                int cnt = 0;
                var bookClub = await _bookClubRepository.GetAsync(req.BookClubId);

                foreach (var user in bookClub?.UserDtos?.OrderBy(x => Guid.NewGuid()))
                {
                    StoryUser su = new()
                    {
                        Story = story,
                        ApplicationUserId = user.Id,
                        Order = cnt
                    };
                    story.StoryUsers.Add(su);
                    cnt++;
                }
            }

            var genre = await _genreRepository.GetAsync(req.GenreId);
            Random rnd = new Random();
            var numberKeywords = rnd.Next(1, 4);

            var thisPartKeywords = new List<KeyWordDetailDto>();
            for (int x = 0; x < numberKeywords; x++)
            {
                var newKeyword = genre.KeyWordDetailDtos?
                    .Where(x => !thisPartKeywords.Any(p => p.Word == x.Word))
                    .OrderBy(x => Guid.NewGuid())
                    .First();
                thisPartKeywords.Add(newKeyword);
            }

            var firstStoryUserId = story.StoryUsers.OrderBy(x => x.Order).First().ApplicationUserId;

            var firstStoryPart = new StoryPart()
            {
                Story = story,
                ApplicationUserId = firstStoryUserId,
                Order = 0,
                IsFinalStoryPart = false,
                IsRhymingRequired = false,
                Text = ""
            };

            var newStoryPartKeyWords = thisPartKeywords.Select(x => new StoryPartKeyWord
            {
                KeyWordId = x.Id,
                StoryPart = firstStoryPart
            }).ToList();

            firstStoryPart.StoryPartKeyWords = newStoryPartKeyWords;

            story.StoryParts.Add(firstStoryPart);

            await _storyRepository.AddAsync(story);

            return Unit.Value;
        }
    }
}