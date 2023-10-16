using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NatCat.Application.Queries.RhymingPattern;
using NatCat.DAL.Entity;

namespace NatCat.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class RhymingPatternController : BaseController
    { 
        public RhymingPatternController(IHttpContextAccessor httpContextAccessor) : base (httpContextAccessor)
		{
		}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RhymingPattern>>> ListAll()
        {
            try
            {
                var items = await Mediator.Send(new ListRhymingPatterns());
                return Ok(items);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}

