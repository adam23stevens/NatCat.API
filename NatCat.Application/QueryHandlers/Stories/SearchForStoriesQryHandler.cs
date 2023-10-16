using System;
using MediatR;
using NatCat.Application.Queries.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.Story;
using NatCat.Model.Enum;

namespace NatCat.Application.QueryHandlers.Stories
{
	public class SearchForStoriesQryHandler : IRequestHandler<SearchForStoriesQry, PagedResult<StoryListDto>>
    {
        private IRepository<Story, StoryDetailDto, StoryListDto> _storyRepository;
        public SearchForStoriesQryHandler(IRepository<Story, StoryDetailDto, StoryListDto> storyRepository)
		{
			_storyRepository = storyRepository;
		}

        public async Task<PagedResult<StoryListDto>> Handle(SearchForStoriesQry searchForStoriesQry, CancellationToken cancellationToken)
        {
            var req = searchForStoriesQry._request;

            int maxStoryPartPercentage = req.storyPosition switch
            {
                StoryPosition.Start => 30,
                StoryPosition.Middle => 60,
                StoryPosition.End => 90,
                _ => 100
            };

            int minStoryPartPercentage = req.storyPosition switch
            {
                StoryPosition.Start => 0,
                StoryPosition.Middle => 30,
                StoryPosition.End => 60,
                _ => 100
            };

            QueryParameters<Story> qry = new()
            {
                wc = story => story.StoryUsers.Count >= req.MinAuthorCount
                           && (string.IsNullOrEmpty(req.TitleSearchText) || story.Title.ToUpper().Contains(req.TitleSearchText.ToUpper()))
                           && (req.GenreIds == null || req.GenreIds.ToList().Contains(story.GenreId))
                           && (req.MaxAuthorCount == 0 || story.StoryUsers.Count() <= req.MaxAuthorCount)
                           //&& (req.StoryTypeIds == null || req.StoryTypeIds.ToList().Contains(story.StoryTypeId))
                           && (story.StoryParts.Count / story.MaxStoryParts) >= minStoryPartPercentage
                           && (story.StoryParts.Count / story.MaxStoryParts) <= maxStoryPartPercentage,
                PageNumber = searchForStoriesQry.PageNumber,
                PageSize = searchForStoriesQry.PageSize
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

