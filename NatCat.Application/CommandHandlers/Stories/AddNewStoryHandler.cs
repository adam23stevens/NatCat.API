using AutoMapper;
using MediatR;
using NatCat.Application.Commands.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.BookClub;
using NatCat.Model.Dto.Story;
using NatCat.Model.Enum;
using NatCat.Model.Web.Story;

namespace NatCat.Application.CommandHandlers.Stories
{
    public class AddNewStoryHandler : IRequestHandler<AddStory>
    {
        private IMapper _mapper;
        private IRepository<BookClub, BookClubDetailDto, BookClubListDto> _bookClubRepository;
        private IRepository<Story, StoryDetailDto, StoryListDto> _storyRepository;
        public AddNewStoryHandler(IMapper mapper,
        IRepository<BookClub, BookClubDetailDto, BookClubListDto> bookClubRepository,
        IRepository<Story, StoryDetailDto, StoryListDto> storyRepository)
        {
            _mapper = mapper;
            _bookClubRepository = bookClubRepository;
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
            
            // story.StoryUsers.Add(new()
            // {
            //     Order = 0,
            //     ApplicationUserId = request.LoggedInUserId,
            //     Story = story
            // });
            story.AuthorApplicationUserId = request.LoggedInUserId;

            if (req.BookClubId != null)
            {
                int cnt = 1;
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

            var firstStoryPart = new StoryPart()
            {
                Story = story,
                ApplicationUserId = request.LoggedInUserId,
                Order = 0,
                IsFinalStoryPart = false,
                IsRhymingRequired = false,
                Text = ""
            };

            story.StoryParts.Add(firstStoryPart);

            await _storyRepository.AddAsync(story);

            return Unit.Value;
        }
    }
}