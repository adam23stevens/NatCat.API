using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NatCat.DAL.Entity
{
    [PrimaryKey(nameof(StoryId), nameof(ApplicationUserId))]
    public class StoryUser
    {
        [ForeignKey(nameof(StoryId))]
        public Guid StoryId { get; set; }
        public virtual Story? Story { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public int Order { get; set; }
    }
}