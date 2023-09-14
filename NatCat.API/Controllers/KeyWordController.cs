using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NatCat.Application.Commands.KeyWords;
using NatCat.Application.Queries.KeyWords;
using NatCat.DAL.Web.Request.Base;
using NatCat.DAL.Web.Request.Base.NatCat.API.Model.Request.KeyWords;
using NatCat.Model.Helper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NatCat.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class KeyWordController : BaseController
    {
        public KeyWordController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        [HttpGet]
        public async Task<IActionResult> List(Guid parentId)
        {
            var keyWords = await Mediator.Send(new ListKeyWords(parentId));
            return Ok(keyWords);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddKeyWordReq req)
        {
            await Mediator.Send(new AddKeyWord(req));
            return Ok();
        }

    }
}

