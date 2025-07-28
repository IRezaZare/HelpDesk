using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Entities
{
    public class Ticket : AuditableEntity
    {
        [Display(Name ="عنوان")]
        public string Title { get; set; }
        [Display(Name ="وضعیت")]

        public TicketStatus Status { get; set; }
        public int Version { get; set; } 
        public bool IsActive { get; set; }

        public int Priority { get; set; }
        public string Description { get; set; }

        
    }
}
public enum TicketStatus
{
    Open,
    Close
}