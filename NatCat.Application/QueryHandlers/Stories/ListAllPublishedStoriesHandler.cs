using MediatR;
using Microsoft.IdentityModel.Tokens;
using NatCat.Application.Queries.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.QueryHandlers.Stories
{
    public class ListAllPublishedStoriesHandler : IRequestHandler<ListAllPublishedStories, PagedResult<StoryListDto>>
    {
        private IRepository<Story, StoryDetailDto, StoryListDto> _storyRepository;
        public ListAllPublishedStoriesHandler(IRepository<Story, StoryDetailDto, StoryListDto> storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<PagedResult<StoryListDto>> Handle(ListAllPublishedStories request, CancellationToken cancellationToken)
        {
            QueryParameters<Story> qry = new()
            {
                wc = p => !p.IsBeingWritten && p.IsVisibleOnLibrary
                && (p.Title.ToUpper().Contains(request.QueryText.ToUpper()) || request.QueryText.IsNullOrEmpty())
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