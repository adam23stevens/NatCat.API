using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NatCat.Application.Queries.MaskingType;
using NatCat.DAL.Entity;

namespace NatCat.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class MaskingTypeController : BaseController
    {
        public MaskingTypeController(IHttpContextAccessor httpContextAccessor) : base (httpContextAccessor)
		{
		}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaskingType>>> ListAll()
        {
            try
            {
                var items = await Mediator.Send(new ListMaskingTypes());
                return Ok(items);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}