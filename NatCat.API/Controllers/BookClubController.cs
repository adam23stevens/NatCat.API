using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.BookClub;
using MediatR;
using NatCat.Application.Queries.BookClubs;
using NatCat.Model.Web.Request.BookClub;
using NatCat.DAL.Entity;
using NatCat.DAL.Contracts;
using NatCat.Application.Commands.BookClubs;

namespace NatCat.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookClubController : BaseController
    {
        public BookClubController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        [HttpGet]
        public async Task<ActionResult<PagedResult<BookClubListDto>>> GetCurrentUserBookClubsPaged()
        { 
            var ret = await Mediator.Send(new BookClubsPaged(LoggedInUserId));
            return Ok(ret);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUserBookClubs()
        {
            var ret = await Mediator.Send(new GetUserBookClubs(LoggedInUserId));
            return Ok(ret);
        }


        [HttpPost]
        public async Task<IActionResult> SendRequestToJoinBookClub([FromBody] SendRequestToJoinBookClubReq request)
        {
            try
            {
                await Mediator.Send(new SendRequestToJoinBookClub(request, LoggedInUserProfileName));

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AcceptToJoinBookClub([FromBody] Guid bookClubJoinRequestId)
        {
            try
            {
                await Mediator.Send(new AcceptRequestToJoinBookClub(bookClubJoinRequestId));
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeclineToJoinBookClub([FromBody] Guid bookClubJoinRequestId)
        {
            try
            {
                await Mediator.Send(new DeclineRequestToJoinBookClub(bookClubJoinRequestId));
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBookClub(Guid Id)
        {
            try
            {
                var bookClubDetail = await Mediator.Send(new GetBookClub(Id));
                return Ok(bookClubDetail);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBookClubRequests()
        {
            try
            {
                var requests = await Mediator.Send(new GetBookClubJoinRequests(LoggedInUserId));
                return Ok(requests);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}