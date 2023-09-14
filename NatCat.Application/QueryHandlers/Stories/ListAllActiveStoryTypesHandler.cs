using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Application.Queries.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.QueryHandlers.Stories
{
    public class ListAllActiveStoryTypesHandler : IRequestHandler<ListAllActiveStoryTypes, IEnumerable<StoryTypeListDto>>
    {
        private IRepository<StoryType, StoryTypeDetailDto, StoryTypeListDto> _storyTypeRepository;
        public ListAllActiveStoryTypesHandler(IRepository<StoryType, StoryTypeDetailDto, StoryTypeListDto> storyTypeRepository)
        {
            _storyTypeRepository = storyTypeRepository;
        }
        public async Task<IEnumerable<StoryTypeListDto>> Handle(ListAllActiveStoryTypes request, CancellationToken cancellationToken)
        {
            var ret = await _storyTypeRepository.ListAllAsync(
                p => true,
                p => p.DisplayOrder
            );

            return ret;
        }
    }
}