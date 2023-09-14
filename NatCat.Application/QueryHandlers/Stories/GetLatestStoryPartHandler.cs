using AutoMapper;
using MediatR;
using Natcat.Web.Response.Story;
using NatCat.Application.Queries.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.QueryHandlers.Stories
{
    public class GetLatestStoryPartQryHandler : IRequestHandler<GetLatestStoryPartQry, LatestStoryPartResponse>
    {
        private IMapper _mapper;
        private readonly IRepository<StoryPart, StoryPartDetailDto, StoryPartListDto> _storyPartRepository;

        public GetLatestStoryPartQryHandler(IMapper mapper, IRepository<StoryPart, StoryPartDetailDto, StoryPartListDto> storyPartRepository)
        {
            _mapper = mapper;
            _storyPartRepository = storyPartRepository;
        }
        public async Task<LatestStoryPartResponse> Handle(GetLatestStoryPartQry request, CancellationToken cancellationToken)
        {
            var latestStoryParts = await _storyPartRepository.EntityListAllAsync(
                p => p.StoryId == request.LatestStoryPartReq.StoryId,
                p => p.Order, true
            );

            if (!latestStoryParts.Any())
            {
                throw new Exception("No story parts found!");
            }

            var ret = _mapper.Map<LatestStoryPartResponse>(
                latestStoryParts.First()
            );

            if (ret.ApplicationUserId != request.LatestStoryPartReq.ApplicationUserId)
            {
                throw new Exception("It is not this user's turn to add to this story.");
            }

            return ret;
        }
    }
}