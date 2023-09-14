using System;
using MediatR;
using NatCat.Model.Web.Request.BookClub;

namespace NatCat.Application.Commands.BookClubs
{
	public class SendRequestToJoinBookClub : IRequest
	{
		public SendRequestToJoinBookClub(
			SendRequestToJoinBookClubReq request,
			string requestingApplicationUserProfileName)
		{
			Request = request;
			RequestingApplicationUserProfileName = requestingApplicationUserProfileName;
		}
		public SendRequestToJoinBookClubReq Request { get; set; }
		public string RequestingApplicationUserProfileName { get; set; }
	}
}

