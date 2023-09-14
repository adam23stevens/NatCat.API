using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Application.Commands.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.CommandHandlers.Stories
{
    public class PublishStoryHandler : IRequestHandler<PublishStory>
    {
        private IRepository<Story, StoryDetailDto, StoryListDto> _storyRepository;
        public PublishStoryHandler(IRepository<Story, StoryDetailDto, StoryListDto> storyRepository)
        {
            _storyRepository = storyRepository;
        }
        public async Task<Unit> Handle(PublishStory request, CancellationToken cancellationToken)
        {
            var story = await _storyRepository.GetEntityAsync(request.Id);
            if (story.DateCompleted == null)
            {
                throw new Exception("Cannot Publish an incomplete Story");
            }

            story.IsVisibleOnLibrary = true;
            story.DatePublished = DateTime.Now;

            await _storyRepository.UpdateAsync(story.Id, story);

            return Unit.Value;
        }
    }
}