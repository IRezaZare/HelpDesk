using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Entities
{
    public class Ticket : AuditableEntity
    {
        public string Title { get; set; }
        public TicketStatus Status { get; set; }
        public int Version { get; set; } 
        public bool IsActive { get; set; }

        public int Priority { get; set; }
        public string Description { get; set; }

        #region Relation
        public List<Comment> Comments { get; set; }

        #endregion
    }
}
public enum TicketStatus
{
    Open,
    Close
}