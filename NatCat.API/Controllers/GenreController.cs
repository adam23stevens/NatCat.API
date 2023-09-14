using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NatCat.DAL.Web.Request.Base;
using NatCat.Model.StaticData;
using NatCat.Application.Queries.Genres;
using NatCat.Application.Commands.Genres;
using NatCat.Model.Dto.Genre;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NatCat.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GenreController : BaseController
    {
        public GenreController(IHttpContextAccessor httpContextAccessor) : base (httpContextAccessor) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreListDto>>> ListAll()
        {
            var allGenres = await Mediator.Send(new GenresList());
            return Ok(allGenres);
        }

        
        [HttpPost]
        public async Task<IActionResult> Add(ReqWithName req)
        {
            var newGenre = await Mediator.Send(new AddGenre(req.Name));
            return Ok();
        }
    }
}

