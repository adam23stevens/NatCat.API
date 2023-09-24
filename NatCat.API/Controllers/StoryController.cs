using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Natcat.Web.Response.Story;
using NatCat.Application.Commands.Stories;
using NatCat.Application.Queries.Stories;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.Story;
using NatCat.Model.Request.Story;
using NatCat.Model.Web.Request.Story;
using NatCat.Model.Web.Story;

namespace NatCat.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class StoryController : BaseController
    {
        public StoryController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        [HttpPost]
        public async Task<ActionResult<PagedResult<StoryListDto>>> SearchStories([FromBody] SearchForStoriesReq request)
        {
            var ret = await Mediator.Send(new SearchForStoriesQry(request));

            return Ok(ret);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<StoryListDto>>> MyActiveStories()
        {
            string userId = LoggedInUserId;
            if (userId is null)
            {
                return BadRequest();
            }

            var ret = await Mediator.Send(new ListUserActiveStories(userId));

            return Ok(ret);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<StoryListDto>>> RecentlyPublished()
        {
            var ret = await Mediator.Send(new ListRecentlyPublishedStories());

            return Ok(ret);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoryTypeListDto>>> AllStoryTypes()
        {
            var ret = await Mediator.Send(new ListAllActiveStoryTypes());

            return Ok(ret);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddStory([FromBody] AddStoryReq req)
        {
            try
            {
                var ret = await Mediator.Send(new AddStory(req, LoggedInUserId));

                return Ok(ret);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<LatestStoryPartResponse>> GetLatestStoryPart(Guid storyId)
        {
            try
            {
                var req = new LatestStoryPartReq()
                {
                    ApplicationUserId = LoggedInUserId,
                    StoryId = storyId
                };

                var ret = await Mediator.Send(new GetLatestStoryPartQry(req));

                if (ret != null)
                {
                    return Ok(ret);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitStoryPart([FromBody] AddStoryPartReq req)
        {
            try
            {
                await Mediator.Send(new AddStoryPart(req, LoggedInUserId));

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> JoinStory(Guid storyId)
        {
            try
            {
                await Mediator.Send(new JoinStory(storyId, LoggedInUserId));

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ReadStoryResponseDto>> ReadStory(Guid storyId)
        {
            try
            {
                var storyRet = await Mediator.Send(new GetStory(storyId));

                return Ok(storyRet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PublishStory([FromBody] PublishStoryReq req)
        {
            try
            {
                await Mediator.Send(new PublishStory(req.Id));
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStoryJoinRequests()
        {
            try
            {
                var requests = await Mediator.Send(new GetStoryJoinRequests(LoggedInUserId));
                return Ok(requests);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}