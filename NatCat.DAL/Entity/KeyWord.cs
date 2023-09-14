using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NatCat.DAL.Entity
{
    public class KeyWord : BaseGuidEntity
    { 
        public string? Word { get; set; }
        public int Difficulty { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Guid GenreId { get; set; }
        public virtual Genre? Genre { get; set; }
        public virtual ICollection<StoryPartKeyWord>? StoryPartKeyWords { get; set; }
    }
}

