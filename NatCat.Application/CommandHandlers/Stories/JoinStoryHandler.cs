using System;
using MediatR;
using NatCat.Application.Commands.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.CommandHandlers.Stories
{
	public class JoinStoryHandler : IRequestHandler<JoinStory>
	{
        private readonly IRepository<Story, StoryDetailDto, StoryListDto> _storyRepository;
		public JoinStoryHandler(IRepository<Story, StoryDetailDto, StoryListDto> storyRepository)
		{
            _storyRepository = storyRepository;
		}

        public async Task<Unit> Handle(JoinStory request, CancellationToken cancellationToken)
        {
            var story = await _storyRepository.GetEntityAsync(request.StoryId);
            if (story.StoryUsers.Count() >= story.MaxUsers)
            {
                throw new Exception("Unable to join. Story is currently full");
            }
            else
            {
                var maxStoryUserOrder = story.StoryUsers.Max(x => x.Order);
                var newStoryUser = new StoryUser()
                {
                    ApplicationUserId = request.ApplicationUserId,
                    Order = maxStoryUserOrder + 1,
                    StoryId = story.Id
                };

                story.StoryUsers.Add(newStoryUser);
                await _storyRepository.UpdateAsync(story.Id, story);
            }
            return Unit.Value;
        }
    }
}

