using System.ComponentModel.DataAnnotations.Schema;

namespace NatCat.DAL.Entity
{
	public class StoryJoinRequest : BaseGuidEntity
	{
		public string? RequestingUserProfileName { get; set; }
        public DateTime DateRequested { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey(nameof(StoryId))]
        public Guid StoryId { get; set; }
        public virtual Story? Story { get; set; }
    }
}

