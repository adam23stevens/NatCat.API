using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NatCat.Application.Queries.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.QueryHandlers.Stories
{
    public class GetStoryHandler : IRequestHandler<GetStory, ReadStoryResponseDto>
    {
        private IMapper _mapper;
        private IRepository<Story, StoryDetailDto, StoryListDto> _storyRepository;
        public GetStoryHandler(IMapper mapper, IRepository<Story, StoryDetailDto, StoryListDto> storyRepository)
        {
            _mapper = mapper;
            _storyRepository = storyRepository;
        }
        public async Task<ReadStoryResponseDto> Handle(GetStory request, CancellationToken cancellationToken)
        {
            try
            {
                var story = await _storyRepository.GetEntityAsync(request.Id,
                            p => p.StoryParts,
                            p => p.StoryUsers);

                if (story.DateCompleted == null)
                {
                    throw new Exception("THis story is not completed yet");
                }
                if (story.DatePublished == null)
                {
                    throw new Exception("This story is not published yet");
                }
                var ret = _mapper.Map<ReadStoryResponseDto>(story);

                return ret;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}