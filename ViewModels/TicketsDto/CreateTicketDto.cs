using AutoMapper;
using HelpDesk.Entities;
using HelpDesk.Mapping;

namespace HelpDesk.ViewModels.TicketsDto
{
    public class CreateTicketDto : IMapFrom<Ticket>
    {
        public string Title { get; set; }
        public TicketStatus Status { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTicketDto, Ticket>();
        }
    }
}
