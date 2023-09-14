namespace NatCat.DAL.Entity
{
    public class Badge : BaseGuidEntity
    {
        public string? Name { get; set; }
        public int Value { get; set; }
        public virtual ICollection<AwardedBadge>? AwardedBadges { get; set; }

    }
}