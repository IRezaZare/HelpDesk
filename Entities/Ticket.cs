﻿namespace HelpDesk.Entities
{
    public class Ticket : AuditableEntity
    {
        public string Title { get; set; }
        public TicketStatus Status { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        
    }
}
public enum TicketStatus
{
    Opent,
    Close
}