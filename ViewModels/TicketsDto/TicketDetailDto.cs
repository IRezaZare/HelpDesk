using HelpDesk.Entities;
using HelpDesk.Mapping;
using System.ComponentModel.DataAnnotations;

namespace HelpDesk.ViewModels.TicketsDto
{
    public class TicketDetailDto : IMapFrom<Ticket>
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "وضعیت")]

        public TicketStatus Status { get; set; }

        public int Priority { get; set; }
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; } 
        public bool IsActive { get; set; } = true;
    }
}
