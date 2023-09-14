using System;
using MediatR;
using NatCat.DAL.Entity;

namespace NatCat.Application.Queries.Users
{
	public class GetAllUsers : IRequest<IEnumerable<ApplicationUser>>
    {
		public GetAllUsers()
		{
		}
	}
}

