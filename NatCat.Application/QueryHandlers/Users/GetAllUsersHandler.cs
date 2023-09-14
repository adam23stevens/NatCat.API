using System;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NatCat.Application.Queries.Users;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.User;

namespace NatCat.Application.QueryHandlers.Users
{
	public class GetAllUsersHandler : IRequestHandler<GetAllUsers, IEnumerable<ApplicationUser>>
	{
        private readonly UserManager<ApplicationUser> _userManager;        
		public GetAllUsersHandler(UserManager<ApplicationUser> userManager)
		{
            _userManager = userManager;
		}

        public async Task<IEnumerable<ApplicationUser>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            return _userManager.Users;
        }
    }
}

