using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NatCat.DAL.Entity
{
    [PrimaryKey(nameof(StoryPartId), nameof(KeyWordId))]
    public class StoryPartKeyWord
    {
        [ForeignKey(nameof(StoryPartId))]
        public Guid StoryPartId { get; set; }
        public virtual StoryPart? StoryPart { get; set; }

        [ForeignKey(nameof(KeyWordId))]
        public Guid KeyWordId { get; set; }
        public virtual KeyWord? KeyWord { get; set; }
    }
}