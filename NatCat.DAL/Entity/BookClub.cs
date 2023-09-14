namespace NatCat.DAL.Entity
{
    public class BookClub : BaseGuidEntity
    {
        public string? Name { get; set; }
        public float AverageRating { get; set; }
        public virtual ICollection<ApplicationUser>? ApplicationUsers { get; set; }
        public virtual ICollection<Story>? Stories { get; set; }
        public virtual ICollection<BookClubJoinRequest>? BookClubJoinRequests { get; set; }
    }
}