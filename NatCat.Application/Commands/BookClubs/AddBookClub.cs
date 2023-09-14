using System;
using MediatR;

namespace NatCat.Application.Commands.BookClubs
{
	public class AddBookClub : IRequest
	{
		public AddBookClub(string name)
		{
			Name = name;
		}
		public string Name { get; set; }
	}
}

