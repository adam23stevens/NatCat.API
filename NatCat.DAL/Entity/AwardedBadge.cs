using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NatCat.DAL.Entity
{
    [PrimaryKey(nameof(BadgeId), nameof(ApplicationUserId))]
    public class AwardedBadge
    {
        public AwardedBadge()
        {
            DateTimeAwarded = DateTime.Now;
        }
        
        [ForeignKey(nameof(BadgeId))]
        public Guid BadgeId {get;set;}
        public virtual Badge? Badge {get;set;}

        [ForeignKey(nameof(ApplicationUserId))]
        public string? ApplicationUserId {get;set;}
        public virtual ApplicationUser? ApplicationUser {get;set;}
        public DateTime DateTimeAwarded {get;set;}
    }
}