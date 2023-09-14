using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NatCat.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private IHttpContextAccessor _httpContextAccessor;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected IMediator Mediator
        {
            get
            {
                if (_mediator == null)
                {
                    _mediator = HttpContext.RequestServices.GetService<IMediator>();
                }
                return _mediator;
            }
        }

        protected string LoggedInUserId =>
            _httpContextAccessor.HttpContext.User.FindFirst("id").Value;

        protected string LoggedInUserProfileName =>
            _httpContextAccessor.HttpContext.User.FindFirst("Name").Value;

    }
}