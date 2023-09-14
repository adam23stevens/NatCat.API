using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Application.Queries.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.QueryHandlers.Stories
{
    public class ListUserActiveStoriesHandler : IRequestHandler<ListUserActiveStories, PagedResult<StoryListDto>>
    {
        private IRepository<Story, StoryDetailDto, StoryListDto> _storyRepository;
        public ListUserActiveStoriesHandler(IRepository<Story, StoryDetailDto, StoryListDto> storyRepository)
        {
            _storyRepository = storyRepository;
        }
        public async Task<PagedResult<StoryListDto>> Handle(ListUserActiveStories request, CancellationToken cancellationToken)
        {
            QueryParameters<Story> qry = new()
            {
                wc = p => p.StoryUsers.Any(x => x.ApplicationUserId == request.UserId)
                      && p.IsBeingWritten
            };

            try
            {
                return await _storyRepository.PagedAsync(qry,
                    p => p.StoryParts,
                    p => p.AuthorApplicationUser,
                    p => p.StoryUsers,
                    p => p.StoryType
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}