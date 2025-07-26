using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
