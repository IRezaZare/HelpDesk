using AutoMapper;
using HelpDesk.Entities;
using HelpDesk.Mapping;
using System.ComponentModel.DataAnnotations;

namespace HelpDesk.ViewModels.TicketsDto
{
    public class EditTicketDto : IMapFrom<Ticket>
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "وضعیت")]
        public int Version { get; set; }
        public bool IsActive { get; set; }
        public TicketStatus Status { get; set; }

        public int Priority { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Ticket, EditTicketDto>().ReverseMap()
            //.AfterMap((dto, ticket) => ticket.Version += 1);
            .ForMember(x => x.Version,
            c => c.MapFrom(v => v.Version + 1));
        }
    }
}
