using System;
using MediatR;
using NatCat.Application.Queries.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.QueryHandlers.Stories
{
	public class ListRecentlyPublishedStoriesHandler : IRequestHandler<ListRecentlyPublishedStories, PagedResult<StoryListDto>>
	{
        private readonly IRepository<Story, StoryDetailDto, StoryListDto> _storyRepository;
		public ListRecentlyPublishedStoriesHandler(IRepository<Story, StoryDetailDto, StoryListDto> storyRepository)
		{
            _storyRepository = storyRepository;
		}

        public async Task<PagedResult<StoryListDto>> Handle(ListRecentlyPublishedStories request, CancellationToken cancellationToken)
        {
            QueryParameters<Story> qry = new()
            {
                wc = p => p.DatePublished != null// && (DateTime.Now - p.DatePublished).Value.TotalDays <= 7
            };

            try
            {
                return await _storyRepository.PagedAsync(qry,
                    p => p.StoryParts,
                    p => p.AuthorApplicationUser,
                    p => p.StoryUsers,
                    p => p.RhymingPattern
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

