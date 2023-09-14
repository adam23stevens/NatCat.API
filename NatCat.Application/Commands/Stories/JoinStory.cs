using System;
using MediatR;

namespace NatCat.Application.Commands.Stories
{
	public class JoinStory : IRequest
	{
		public JoinStory(Guid storyId, string applicationUserId)
		{
			StoryId = storyId;
			ApplicationUserId = applicationUserId;
		}

		public Guid StoryId { get; set; }
		public string ApplicationUserId { get; set; }
	}
}

